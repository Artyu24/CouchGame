using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectPlayerCentre : MonoBehaviour
{
    [Tooltip("Temps en seconde que doit rester le joueur dans la zone pour éjecter le joueur dans le centre")]
    [SerializeField]
    private float SecondToEject;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.playerInMiddle != null)
        {
            //playerCentre.SwitchModePlayerToModeCentre(false);
            GameManager.instance.playerInMiddle.transform.position = GameManager.instance.RandomSpawn().position;
            GameManager.instance.playerInMiddle.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
            GameManager.instance.playerInMiddle = null;
            Debug.Log("Player au centre éjecté !");
            //Destroy(this);
        }
    }

    private IEnumerator WaitForExpulse()
    {
        yield return new WaitForSeconds(SecondToEject);
    }
}
