using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    public Color activeColor;

    void Start()
    {
        GetComponent<MeshRenderer>().material.color = activeColor;
    }
}