using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class copiText : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = GetComponentInParent<Text>().text;
    }
}
