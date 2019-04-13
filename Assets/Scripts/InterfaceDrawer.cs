using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceDrawer : MonoBehaviour
{
    private Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DrawLines(List<Vector2> dots)
    {
        
    }
}
