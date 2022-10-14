using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocWave : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public float radiusMax;
    public float growingSpeed;
    //public GameObject player;
    public float pushForce;
    bool getPushed = false;
    public Transform transparence;

    private void Start()
    {        
        transparence.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if(transparence.localScale.x < radiusMax)
        {            
            transparence.localScale = new Vector3(transparence.localScale.x + Time.deltaTime * growingSpeed, transparence.localScale.y + Time.deltaTime * growingSpeed, transparence.localScale.z + Time.deltaTime * growingSpeed);
        }
        //if (getPushed == true)
        //{
        //    //Destroy(gameObject);
        //    //a changer quand on aura les player
        //    //mettre un bool qui empeche les joueur qui ont deja été touché d'etre re touché par le collider
        //}

        if(transparence.localScale.x >= radiusMax)
        {
            getPushed = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(getPushed == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Vector3 push = (other.transform.position - sphereCollider.transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(push * pushForce);
                getPushed = true;

            }
        }
        
    }
}
