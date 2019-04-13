using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    public float Health = 100.0f;
    public float healthBarLength;

    void Start()
    {
        healthBarLength = Screen.width / 2;
    }

    void Update()
    {
        AddJustCurrentHealth(0);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10,10,healthBarLength, 20), Health + "/"+MaxHealth);
    }

    public void AddJustCurrentHealth(int odj)
    {
        Health += odj;
        if (Health < 1.0f)
            Health = 0.0f;

        if (Health>MaxHealth)
            Health = MaxHealth;

        if (MaxHealth < 1.0f)
            MaxHealth = 1.0f;
        healthBarLength = (Screen.width / 2) * (Health / (float)MaxHealth);
    }
}
