using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowWater : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(ObjectManager.Instance.DestroyObject(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerData = other.gameObject.GetComponent<Player>();
            if (playerData.isInvincible == false)
            {
                StopAllCoroutines();
                PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                
                if (playerData.IsSpeedUp)
                    playerMovement.Speed = GameManager.instance.MoveSpeed;
                else
                    playerMovement.Speed = GameManager.instance.MinMoveSpeed;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerData = other.gameObject.GetComponent<Player>();
            if (playerData.isInvincible == false)
            {
                StartCoroutine(TimeSlowByWater(playerData));
            }
        }
    }

    public IEnumerator TimeSlowByWater(Player playerData)
    {
        yield return new WaitForSeconds(GameManager.instance.SlowDuration);

        PlayerMovement playerMovement = playerData.gameObject.GetComponent<PlayerMovement>();

        if (playerData.IsSpeedUp)
            playerMovement.Speed = GameManager.instance.MaxMoveSpeed;
        else
            playerMovement.Speed = GameManager.instance.MoveSpeed;
    }
}
