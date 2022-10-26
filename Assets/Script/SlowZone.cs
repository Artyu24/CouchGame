using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    Player player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().MovementSpeed = GameManager.instance.MovSpeedSlowZone; 

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().MovementSpeed = GameManager.instance.MaxMovementSpeed;

        }

    }

}
