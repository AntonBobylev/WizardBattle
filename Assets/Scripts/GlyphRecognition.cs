using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GlyphRecognition: MonoBehaviour
{
    GlyphsDictionary dictionary = new GlyphsDictionary();
    public static KeyCode writeButton = KeyCode.C; // клавиша для рисования глифа
    public static KeyCode createButton = KeyCode.V; // клавиша для создания шаблона глифа
    public int pointsCount = 100; //кол-во значимых точек в глифе
    static public double maxGlyphDifference = 3000; //максимальная расходимость глифов в единицах
    private double lastGlyphPreccision = 0; //процент правильности последнего глифа
    private List<GlyphPoint> inputGlyph = new List<GlyphPoint>();

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DrawGlyph());
    }
    IEnumerator DrawGlyph()
    {
        List<GlyphPoint> cpList;
        KeyCode controlButton;
        inputGlyph.Clear();
        cpList = inputGlyph;
        if (Input.GetKeyDown(createButton))
        {
            controlButton = createButton;
        }
        else if(Input.GetKeyDown(writeButton))
        {
            controlButton = writeButton;
        }
        else
        {
            yield break;
        }
        //пока зажата кнопка фиксируем положения мыши
        List<Vector2> screenDots = new List<Vector2>();
        while (Input.GetKey(controlButton))
        {
            screenDots.Add(Input.mousePosition);
            yield return new WaitForSeconds(0.1f);
        }
        //если есть точки
        if(screenDots.Count != 0)
        {
            //нормализация, дабы привести нарисованный глиф к ограничению кол-ва точек
            NormaliseDots(screenDots);
            cpList.Add(new GlyphPoint(new Vector2(1,1), 0.0f));
            for (int i = 1; i < screenDots.Count; i++)
            {
                Vector2 tmpVec = screenDots[i] - screenDots[i - 1];
                Vector2 lastVec = cpList[cpList.Count - 1].vector;
                double cosA = (tmpVec.x * lastVec.x + tmpVec.y * lastVec.y) / (tmpVec.magnitude * lastVec.magnitude);
                double angle = (Math.Acos(cosA) * 180 / Math.PI);
                if (double.IsNaN(angle)) angle = 0;
                //вектор смещения относительно предыдущей точки и угол поворота
                cpList.Add(new GlyphPoint(tmpVec, angle));
            }
            if(controlButton == writeButton)
            {
                Tuple<double, Glyph> tuple = dictionary.FindGlyph(new Glyph("", inputGlyph));
                double difference = maxGlyphDifference - tuple.Item1;
                lastGlyphPreccision = (difference < 0 ? 0 : difference)/maxGlyphDifference*100;
                Debug.Log("Glyph precision: " + lastGlyphPreccision);
                Debug.Log("Glyph name: " + tuple.Item2.glyphName);
                if (tuple.Item2.glyphName.Length != 0)
                {
                    Type.GetType("GlyphFunctions").GetMethod(tuple.Item2.glyphName).Invoke(null, new object[] { gameObject });
                }
                yield break;
            }
            else
            {
                new Glyph("blink", cpList).ExportGlyph();
                dictionary.ImportGlyphs();
            }
        }
    }
    //приведение полученного кол-ва точек к ограниченному
    void NormaliseDots(List<Vector2> list)
    {
        while (list.Count > pointsCount)
        {//при большем кол-ве, удаление точек с минимальным расстоянием и смещение результирующей
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
        {//при меньшем кол-ве, добавление точек между точками с макс расстоянием
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
    
}

