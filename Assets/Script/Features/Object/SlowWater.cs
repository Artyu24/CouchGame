using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowWater : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<Player>().isInvincible == false)
            {
                other.gameObject.GetComponent<PlayerMovement>().MovementSpeed = GameManager.instance.MovSpeedSlowZone;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<Player>().isInvincible == false)
            {
                StartCoroutine(TimeSlowByWater(other));
            }
            
        }
    }

    public IEnumerator TimeSlowByWater(Collider other)
    {
        yield return new WaitForSeconds(GameManager.instance.SlowDuration);
        other.gameObject.GetComponent<PlayerMovement>().MovementSpeed = GameManager.instance.MaxMovementSpeed;
    }
}
