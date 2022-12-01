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
        if (other.CompareTag("Player") && CenterManager.instance.ActualCenterState == CenterState.USE)
        {
            GameManager.instance.ejectPlatesActive++;
            bc.enabled = false;
            //GetComponentInChildren<MeshRenderer>().material.color = GameManager.instance.ActivatedColor;
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreInterrupteur, other.GetComponent<Player>());

            GetComponent<Animator>().SetTrigger("Press");

            if (GameManager.instance.ejectPlatesActive >= GameManager.instance.NumberOfPlate)
            {
                EjectPlayer();
                CenterManager.instance.DesactivateAllBridge();
            }
        }
    }

    public void EjectPlayer()
    {
        GameManager GM = GameManager.instance;

        GM.PlayerInMiddle.transform.position = PointAreaManager.instance.GetPlayerRandomPos().position;
        GM.PlayerInMiddle.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
        GM.PlayerInMiddle.GetComponent<Player>().HideGuy(true);

        for (int i = 0; i < GameManager.instance.tabCircle.Count; i++)
        {
            GM.tabCircle[i].GetComponent<MeshRenderer>().material.color = GameManager.instance.TabMaterialColor[i];
            GM.tabCircle[i].GetComponent<Outline>().enabled = false;
        }

        GM.ejectPlatesActive = 0;

        foreach (GameObject plate in GM.EjectPlates)
            Destroy(plate);

        GM.EjectPlates.Clear();

        GM.PlayerInMiddle = null;
        Debug.Log("Player au centre éjecté !");
    }
}
