using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpObject : MonoBehaviour, IPickable
{
    private void Awake()
    {
        StartCoroutine(ObjectManager.Instance.DestroyObject(gameObject));
    }

    public void Interact(Player player = null)
    {
        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if(player.IsSlow) 
                playerMovement.Speed = GameManager.instance.MoveSpeed;
            else
                playerMovement.Speed = GameManager.instance.MaxMoveSpeed;
            
            ObjectManager.Instance.StopSpeedUp(playerMovement);
            player.IsSpeedUp = true;
            Destroy(this.gameObject);
        }
    }
}