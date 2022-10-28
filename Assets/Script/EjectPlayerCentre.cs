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
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreInterrupteur, other.GetComponent<Player>());

            if (GameManager.instance.ejectPlatesActive >= GameManager.instance.NumberOfPlate)
            {
                EjectPlayer();
            }
        }
    }

    public void EjectPlayer()
    {
        GameManager GM = GameManager.instance;

        GM.PlayerInMiddle.transform.position = GM.RandomSpawn().position;
        GM.PlayerInMiddle.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
        GM.PlayerInMiddle.GetComponent<Player>().HideGuy(true);
        GM.PlayerInMiddle.GetComponent<Rigidbody>().useGravity = true;

        for (int i = 0; i < GameManager.instance.TabCircle.Count; i++)
        {
            GM.TabCircle[i].GetComponent<MeshRenderer>().material.color = GameManager.instance.TabMaterialColor[i];
            GM.TabCircle[i].GetComponent<Outline>().enabled = false;
        }

        GM.ejectPlatesActive = 0;

        foreach (GameObject plate in GM.EjectPlates)
            Destroy(plate);

        GM.EjectPlates.Clear();

        GM.PlayerInMiddle = null;
        Debug.Log("Player au centre éjecté !");
    }
}
