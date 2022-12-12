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
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                
            if (playerData.IsSpeedUp)
            {
                playerMovement.Speed = GameManager.instance.MoveSpeed;
                playerMovement.animator.SetFloat("RunModifier", 1f);
            }
            else
            {
                playerMovement.Speed = GameManager.instance.MinMoveSpeed; 
                playerMovement.animator.SetFloat("RunModifier", .5f);
            }

            playerData.IsSlow = true;
            playersInside.Add(playerMovement);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerData = other.gameObject.GetComponent<Player>();
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            
            if (playerData.IsSpeedUp)
            {
                playerMovement.Speed = GameManager.instance.MaxMoveSpeed; 
                playerMovement.animator.SetFloat("RunModifier", 2f);
            }
            else
            {
                playerMovement.Speed = GameManager.instance.MoveSpeed; 
                playerMovement.animator.SetFloat("RunModifier", 1f);
            }

            playerData.IsSlow = false;
            playersInside.Remove(playerMovement);
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
