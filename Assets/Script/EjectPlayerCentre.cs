using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectPlayerCentre : MonoBehaviour
{
    private BoxCollider bc;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.PlayerInMiddle != null)
        {
            GameManager.instance.ejectPlatesActive++;
            bc.enabled = false;
            GetComponentInChildren<MeshRenderer>().material.color = GameManager.instance.ActivatedColor;

            if (GameManager.instance.ejectPlatesActive >= GameManager.instance.NumberOfPlate)
            {
                GameManager.instance.PlayerInMiddle.transform.position = GameManager.instance.RandomSpawn().position;
                GameManager.instance.PlayerInMiddle.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
                GameManager.instance.PlayerInMiddle.GetComponent<Player>().HideGuy(true);

                for (int i = 0; i < GameManager.instance.TabCircle.Length; i++)
	            {
                    GameManager.instance.TabCircle[i].GetComponent<MeshRenderer>().material.color = GameManager.instance.TabMaterialColor[i];
                    GameManager.instance.TabCircle[i].GetComponent<Outline>().enabled = false;
	            }

                for (int i = 0; i < GameManager.instance.EjectPlates.Length; i++)
                {
                    GameManager.instance.ejectPlatesActive = 0;
                    GameManager.instance.EjectPlates[i].GetComponentInChildren<MeshRenderer>().material.color = GameManager.instance.ActiveColor;
                    GameManager.instance.EjectPlates[i].SetActive(false);
                }
                GameManager.instance.PlayerInMiddle = null;
                Debug.Log("Player au centre éjecté !");
            }
        }
    }
}
