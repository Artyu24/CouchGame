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
            player.Kill();
            int xcount = Random.Range(0, 3);

            switch (xcount)
            {
                case 0:
                    FindObjectOfType<AudioManager>().Play("Fall1");
                    break;
                case 1:
                    FindObjectOfType<AudioManager>().Play("Fall2");
                    break;
                case 2:
                    FindObjectOfType<AudioManager>().Play("Fall3");
                    break;
                case 3:
                    FindObjectOfType<AudioManager>().Play("Fall4");
                    break;
            }
        }
    }
}
