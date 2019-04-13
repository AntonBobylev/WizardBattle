using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
    public GameObject Player;//Объект игрок
    public float HitValue = 5.0f;
    public float timer = 1.0f;

    void Start()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            timer -= 0.01f;
        if (timer < 0)
        {
            //Нанесение урона
            //GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().Health -= HitValue;
            timer = 1.0f;
        }
    } 

    void OnTriggerEnter(Collider other)
    {
        
            
    }

    void Update()
    {

    }
}
