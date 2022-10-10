using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    [Header("----------Mettre toutes les target pour les météorites----------")]
    [SerializeField] Transform[] position;
    public  float speed;
    Transform nextPos;

    public bool targetFind = false; 

    
    void Update()
    {
        if(targetFind == false)
        {
            StartCoroutine(moveObject());
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
    }

    public  IEnumerator moveObject()
    {
        int ran = Random.Range(1, 5);
        switch (ran)
        {
            
            case 1:
                targetFind = true;
                nextPos = position[0];
                yield return new WaitForSeconds(11.5f);
                targetFind = false;
                break;

            case 2:
                targetFind = true;
                nextPos = position[1];
                yield return new WaitForSeconds(11.5f);
                targetFind = false;
                break;

            case 3:
                targetFind = true;
                nextPos = position[2];
                yield return new WaitForSeconds(11.5f);
                targetFind = false;                             
                break;

            case 4:
                targetFind = true;
                nextPos = position[3];
                yield return new WaitForSeconds(11.5f);
                targetFind = false;
                break;
        }
        yield break;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    
}
