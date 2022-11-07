using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    [Header("----------Mettre toutes les target pour les météorites----------")]
    //[SerializeField] Transform[] position;
    private Vector3 position1 = new Vector3(-6.55f, 0f, 0f);
    private Vector3 position2 = new Vector3(0f, 6.55f, 0f);
    private Vector3 position3 = new Vector3(6.55f, 0f, 0f);
    private Vector3 position4 = new Vector3(0f, -6.55f, 0f);

    Vector3 nextPos;
    public bool explosion = false;
    private GameObject explos;


    private void Awake()
    {
        explos = Resources.Load<GameObject>("explosion");

    }
    void Update()
    {
        if(nextPos != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, GameManager.instance.SpeedMeteorite * Time.deltaTime);
        }
    }
    public void MovePlanete(int i)
    {
        
        switch (i)
        {

            case 1:
                nextPos = position1;
                break;

            case 2:
                nextPos = position2;
                break;

            case 3:
                nextPos = position3;
                break;

            case 4:
                nextPos = position4;
                break;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        Instantiate(explos, nextPos, quaternion.identity);
        Destroy(gameObject);
        

    }

}
