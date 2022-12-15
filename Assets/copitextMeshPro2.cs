using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class copitextMeshPro2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();   

    }
    void Update()
    {
        text.text = transform.parent.GetComponent<TextMeshProUGUI>().text;

    }
}
