using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpObject : MonoBehaviour, IInteractable
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
            playerMovement.Speed = GameManager.instance.MaxMoveSpeed;
            Destroy(this.gameObject);
            StartCoroutine(ObjectManager.Instance.StopSpeedUp(playerMovement));
        }
    }
}
