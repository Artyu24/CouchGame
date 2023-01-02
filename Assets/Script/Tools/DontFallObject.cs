using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontFallObject : MonoBehaviour
{
    private Rigidbody rb;
    void Update()
    {
        if (transform.position.y < -5)
        {
            if(rb == null)
                rb = GetComponent<Rigidbody>();

            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            rb.velocity = Vector3.zero;
        }  
    }
}
