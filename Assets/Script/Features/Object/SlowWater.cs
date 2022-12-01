using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowWater : MonoBehaviour
{
    private List<PlayerMovement> playersInside = new List<PlayerMovement>();

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
                PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                
                if (playerData.IsSpeedUp)
                    playerMovement.Speed = GameManager.instance.MoveSpeed;
                else
                    playerMovement.Speed = GameManager.instance.MinMoveSpeed;

                playerData.IsSlow = true;
                playersInside.Add(playerMovement);
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
                PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                playersInside.Remove(playerMovement);
                
                if (playerData.IsSpeedUp)
                    playerMovement.Speed = GameManager.instance.MaxMoveSpeed;
                else
                    playerMovement.Speed = GameManager.instance.MoveSpeed;

                playerData.IsSlow = false;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (PlayerMovement playerMovement in playersInside)
        {
            Player playerData = playerMovement.GetComponent<Player>();

            if (playerData.IsSpeedUp)
                playerMovement.Speed = GameManager.instance.MaxMoveSpeed;
            else
                playerMovement.Speed = GameManager.instance.MoveSpeed;
        }
    }
}
