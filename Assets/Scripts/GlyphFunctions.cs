using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GlyphFunctions
{
    public static void test(GameObject whoami)
    {
        whoami.transform.Translate(0,5,0);
        Debug.Log("TEST GLYPH ACTION");
    }
    public static void blink(GameObject whoami)
    {
        Vector3 vec = whoami.transform.position + whoami.transform.TransformVector(whoami.transform.Find("Capsule").forward) * 6;
        whoami.transform.position = vec;
        Debug.Log("BLINK GLYPH ACTION");
    }
    public static void space(GameObject whoami)
    {
        whoami.transform.Translate(0, 25, 0);
        Debug.Log("SPACE GLYPH ACTION");
    }
    public static void shield(GameObject whoami)
    {
        CreateShield(whoami);
        Debug.Log("SHIELD GLYPH ACTION");
    }
    private static void CreateShield(GameObject whoami)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 vec = whoami.transform.position + whoami.transform.TransformVector(whoami.transform.Find("Capsule").forward) * 2;
        cube.transform.position = vec;
        GameObject.Destroy(cube, 3);
    }
}