using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public Vector3 nextPos;
    Vector3 explosionPose;
    public Vector3 NextPos { get { return nextPos; }set { nextPos = value; } }
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
    
    private void OnCollisionEnter(Collision collision)
    {
        explosionPose = new Vector3(nextPos.x, nextPos.y - 1, nextPos.z);
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.MeteoriteSound);
        Instantiate(explos, explosionPose, quaternion.identity);
        Destroy(gameObject);       

    }

    
}
