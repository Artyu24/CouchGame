using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocWave : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public float radiusMax;
    public float growingSpeed;
    public GameObject player;
    public float pushForce;
    bool getPushed = false;

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
        if (getPushed == true)
        {
            Destroy(gameObject);
            //a changer quand on aura les player
            //mettre un bool qui empeche les joueur qui ont deja été touché d'etre re touché par le collider
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            Vector3 push = (player.transform.position -sphereCollider.transform.position).normalized;
            player.GetComponent<Rigidbody>().AddForce(push*pushForce);
            getPushed = true;

        }
    }
}
