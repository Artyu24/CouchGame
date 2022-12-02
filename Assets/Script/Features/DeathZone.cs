using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.ActualPlayerState = PlayerState.DEAD;
            CameraManager.Instance.RemovePlayerTarget(player.playerID + 1);
            player.Kill();
            int xcount = Random.Range(0, 3);
            FindObjectOfType<AudioManager>().PlayRandom(SoundState.FallSound);            
        }
    }
}
