using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Simple_Magik_Key : MonoBehaviour
{
    GameObject Player;
    private float RangeStreamDamage = 6.0f;//Длина потока
   
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    IEnumerator StartCastMagicElement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            yield return null;
            CreateProjectile();
        }
    }

    void CreateProjectile()
    {
        // Object ball =  Resources.Load(GameObject.FindWithTag("FireBall").name);
        Object ball = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Skill1.prefab", typeof(GameObject));
        Animation.Crossfade
        if (!ball)
        {
            Debug.Log("Not loaded");
        }

        GameObject.Instantiate(ball, transform.position + new Vector3(0.0f, 0.0f, 0.5f), Player.transform.rotation);
    }//Создание снаряда

    IEnumerator CreateStream()//Создание потока
    {

        RaycastHit hit;
        while (Input.GetKey(KeyCode.Alpha2))
        {
            if (Physics.Raycast(transform.position, Player.transform.forward, out hit, RangeStreamDamage))
            {
                print("Found an object " + hit.transform.name + "distance: " + hit.distance);
            }
            yield return null;
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
                 
            Debug.Log("Begin Magic");
            StartCoroutine(StartCastMagicElement());
        
    }
}
