using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlowWater : MonoBehaviour
{
    private List<PlayerMovement> playersInside = new List<PlayerMovement>();

    private void Awake()
    {
        transform.GetChild(0).localScale = new Vector3(0.05f, 0.05f, 0.05f);
        transform.GetChild(0).DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.InCubic);
        StartCoroutine(ObjectManager.Instance.DestroyObject(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerData = other.gameObject.GetComponent<Player>();
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            playerData.IsSlow = true;
            playerMovement.ChangeSpeed();
            playersInside.Add(playerMovement);
            FindObjectOfType<AudioManager>().PlayRandom(SoundState.EauToxiquePlayerInSound);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerData = other.gameObject.GetComponent<Player>();
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            playerData.IsSlow = false;
            playerMovement.ChangeSpeed();
            playersInside.Remove(playerMovement);
        }
    }

    private void OnDestroy()
    {
        foreach (PlayerMovement playerMovement in playersInside)
        {
            Player playerData = playerMovement.GetComponent<Player>();
            playerData.IsSlow = false;
            playerMovement.ChangeSpeed();
        }
    }
}
