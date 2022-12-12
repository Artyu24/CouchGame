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
            player.IsSpeedUp = true;
            playerMovement.ChangeSpeed();
            ObjectManager.Instance.StopSpeedUp(playerMovement);
            Destroy(this.gameObject);
        }
    }
}
