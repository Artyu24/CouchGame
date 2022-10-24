using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    private Color activeColor;
    private float red, green, blue;

    void Start()
    {
        red = Random.Range(0, 256);
        green = Random.Range(0, 256);
        blue = Random.Range(0, 256);
        activeColor.r = red;
        activeColor.b = blue;
        activeColor.g = green;
        GetComponent<MeshRenderer>().material.color = activeColor;
    }
}