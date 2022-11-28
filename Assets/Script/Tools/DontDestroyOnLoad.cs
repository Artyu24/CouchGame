using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        this.transform.parent = null;
        DontDestroyOnLoad(this);  
    } 
}
