using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowWater : MonoBehaviour
{
    PlayerMovement playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().MovementSpeed = GameManager.instance.MovSpeedSlowZone;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TimeSlowByWater(other));
        }
    }

    public IEnumerator TimeSlowByWater(Collider other)
    {
        yield return new WaitForSeconds(GameManager.instance.SlowDuration);
        other.gameObject.GetComponent<PlayerMovement>().MovementSpeed = GameManager.instance.MaxMovementSpeed;
    }
}
