using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    [Header("----------Mettre toutes les target pour les météorites----------")]
    [SerializeField] Transform[] position;
    public  float speed;
    Transform nextPos;
    public bool explosion = false;
    ChocWave choc;
    public GameObject explos;

    void Update()
    {
        if(nextPos != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        }
    }
    public void MovePlanete(int i)
    {
        
        switch (i)
        {

            case 1:
                nextPos = position[0];
                break;

            case 2:
                nextPos = position[1];
                break;

            case 3:
                nextPos = position[2];
                break;

            case 4:
                nextPos = position[3];
                break;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        Instantiate(explos, nextPos.position, nextPos.rotation);
        Destroy(gameObject);
        

    }

}
