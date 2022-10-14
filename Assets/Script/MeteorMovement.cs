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
    public bool targetFind = false;
    
    void Update()
    {
        if(targetFind == false)
        {            
            MovePlanete();
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
    }
    void MovePlanete()
    {
        int ran = Random.Range(1, 5);
        switch (ran)
        {

            case 1:
                targetFind = true;
                nextPos = position[0];
                break;

            case 2:
                targetFind = true;
                nextPos = position[1];
                break;

            case 3:
                targetFind = true;
                nextPos = position[2];
                break;

            case 4:
                targetFind = true;
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
