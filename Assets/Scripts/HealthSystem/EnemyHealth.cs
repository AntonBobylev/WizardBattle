using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    public float Health = 100.0f;

    void Start()
    {
    }

    void Update()
    {
        AddJustCurrentHealth(0);
        if(this.Health <=0)
        {
            //Анимация смерти
            //Через некоторое время
            Destroy(gameObject);
        }
    }

    public void AddJustCurrentHealth(int odj)
    {
       // Debug.Log("ZASHEL");
        Health += odj;
        if (Health < 1.0f)
            Health = 0.0f;

        if (Health>MaxHealth)
            Health = MaxHealth;

        if (MaxHealth < 1.0f)
            MaxHealth = 1.0f;
    }
}
