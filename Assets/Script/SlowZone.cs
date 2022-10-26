using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{

    
    private void Start()
    {
        GameManager.instance.MovementSpeed = GameManager.instance.MaxMovementSpeed;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.MovementSpeed = GameManager.instance.MovSpeedSlowZone; 

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.MovementSpeed = GameManager.instance.MaxMovementSpeed;

        }

    }

}
