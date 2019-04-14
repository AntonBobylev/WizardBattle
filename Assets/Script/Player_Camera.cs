using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public class MoveMouse : MonoBehaviour
    {
        private Vector3 MousePos;
        void Update()
        {
            MousePos = Input.mousePosition;
        }
    }
}
