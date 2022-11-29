using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierObject : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        player.Multiplier = true;
        StartCoroutine(ObjectManager.Instance.StopMultiplier(player));
    }
}
