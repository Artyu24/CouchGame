using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectPlayerCentre : MonoBehaviour
{
    [Header("Variables Game Feel")]
    [Tooltip("Couleur que prend la zone quand elle est activée")]
    public Color activatedColor;
    [Tooltip("Nombre de plaques à activer pour eject le joueur au centre")]
    public int numberOfPlate = 3;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.PlayerInMiddle != null)
        {
            GameManager.instance.ejectPlatesActive++;
            GetComponentInChildren<MeshRenderer>().material.color = activatedColor;

            if (GameManager.instance.ejectPlatesActive >= numberOfPlate)
            {
                GameManager.instance.PlayerInMiddle.transform.position = GameManager.instance.RandomSpawn().position;
                GameManager.instance.PlayerInMiddle.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
                GameManager.instance.PlayerInMiddle.GetComponent<Player>().HideGuy(true);
                GameManager.instance.PlayerInMiddle = null;
                Debug.Log("Player au centre éjecté !");
                //Destroy(this);
                this.gameObject.SetActive(false);
            }
        }
    }
}
