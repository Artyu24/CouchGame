using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierObject : MonoBehaviour, IInteractable
{
    private void Awake()
    {
        StartCoroutine(ObjectManager.Instance.DestroyObject(gameObject));
    }

    public void Interact(Player player)
    {
        player.Multiplier = true;
        Destroy(this.gameObject);
        StartCoroutine(ObjectManager.Instance.StopMultiplier(player));
    }
}
