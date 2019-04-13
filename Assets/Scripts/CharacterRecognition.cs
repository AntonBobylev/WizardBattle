using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CharacterPoint
{
    public Vector2 vector = new Vector2(0,0);
    public float angle = 0.0f;
    public CharacterPoint(Vector3 _vec, float _angle)
    {
        vector = _vec;
        angle = _angle;
    }

    public double getDifference(CharacterPoint cp)
    {//разница идет в пикселях  
        double difference = 0;
        difference = vector.magnitude - cp.vector.magnitude;
        return Math.Abs(difference);
    }

    override
    public string ToString()
    {
        return vector.ToString() + " " + angle;
    }
}

public class CharacterRecognition: MonoBehaviour
{
    public KeyCode writeButton = KeyCode.C; // клавиша для прыжка
    public KeyCode createButton = KeyCode.V; // клавиша для прыжка
    public int pointsCount = 100;
    private List<CharacterPoint> defCharacter = new List<CharacterPoint>();
    private List<CharacterPoint> writeCharacter = new List<CharacterPoint>();

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CreateDefaultCorontine());
    }

    IEnumerator CreateDefaultCorontine()
    {
        List<CharacterPoint> cpList;
        KeyCode controlButton;
        if (Input.GetKeyDown(createButton))
        {
            defCharacter.Clear();
            cpList = defCharacter;
            controlButton = createButton;
        }
        else if(Input.GetKeyDown(writeButton))
        {
            writeCharacter.Clear();
            cpList = writeCharacter;
            controlButton = writeButton;
        }
        else
        {
            yield break;
        }
        List<Vector2> screenDots = new List<Vector2>();
        while (Input.GetKey(controlButton))
        {
            screenDots.Add(Input.mousePosition);
            yield return new WaitForSeconds(0.1f);
        }
        if(screenDots.Count != 0)
        {
            NormaliseDots(screenDots);
            cpList.Add(new CharacterPoint(new Vector2(1,1), 0.0f));
            for (int i = 1; i < screenDots.Count; i++)
            {
                Vector2 tmpVec = screenDots[i] - screenDots[i - 1];
                Vector2 lastVec = cpList[cpList.Count - 1].vector;
                //некорректно считается угол
                float angle = (float)Math.Acos((tmpVec.x * lastVec.x + tmpVec.y * lastVec.y) / Math.Sqrt(tmpVec.sqrMagnitude * lastVec.sqrMagnitude));
                cpList.Add(new CharacterPoint(tmpVec, angle));
            }
            if(cpList == writeCharacter)
            {
                Debug.Log(ProcessCharacter(writeCharacter, defCharacter));
                yield break;
            }
        }
    }

    void NormaliseDots(List<Vector2> list)
    {
        while (list.Count > pointsCount)
        {
            int minIndex = 1;
            Vector2 minRange = list[1] - list[0];
            for(int i = 2; i<list.Count; i++)
            {
                if((list[i]-list[i-1]).sqrMagnitude < minRange.sqrMagnitude)
                {
                    minIndex = i;
                    minRange = list[i] - list[i - 1];
                }
            }
            list[minIndex] -= minRange / 2;
            list.RemoveAt(minIndex - 1);
        }
        while(list.Count < pointsCount)
        {
            int maxIndex = 1;
            Vector2 maxRange = list[1] - list[0];
            for (int i = 2; i < list.Count; i++)
            {
                if ((list[i] - list[i - 1]).sqrMagnitude > maxRange.sqrMagnitude)
                {
                    maxIndex = i;
                    maxRange = list[i] - list[i - 1];
                }
            }
            list.Insert(maxIndex, list[maxIndex] - maxRange / 2);
        }
    }

    

    double ProcessCharacter(List<CharacterPoint> list, List<CharacterPoint> example)
    {
        double difference = 0;
        for (int i = 0; i<pointsCount; i++)
        {
            difference += list[i].getDifference(example[i]);
        }
        return difference;
    }
}

