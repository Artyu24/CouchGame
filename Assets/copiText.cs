using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class copiText : MonoBehaviour
{
    public Text text;
    public Text text2;

    private void Start()
    {
        text = GetComponent<Text>();
        text2 = GetComponentInParent<Text>();

    }
    void Update()
    {
        text.text = transform.parent.GetComponent<Text>().text;

    }
}
