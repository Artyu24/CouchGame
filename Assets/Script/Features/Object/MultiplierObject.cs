using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierObject : MonoBehaviour, IPickable
{
    private void Awake()
    {
        StartCoroutine(ObjectManager.Instance.DestroyObject(gameObject));
    }

    public void Interact(Player player)
    {
        player.Multiplier = true;
        ScoreManager.instance.scorePlayerText[player.playerID].transform.GetChild(0).gameObject.SetActive(true);
        ObjectManager.Instance.StopMultiplier(player);
        Destroy(this.gameObject);
    }
}
