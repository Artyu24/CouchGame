using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocWave : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public float radiusMax;
    public float growingSpeed;

    private void Start()
    {
        sphereCollider.radius = 0f;
    }

    private void Update()
    {
        if(sphereCollider.radius < radiusMax)
        {
            sphereCollider.radius = sphereCollider.radius + Time.deltaTime * growingSpeed;           

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
}
