using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EjectPlayerCentre : MonoBehaviour
{
    private BoxCollider bc;
    public GameObject cable;
    private Material cableDone;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        cableDone = Resources.Load<Material>("holoMat 1");
    }

    private void Update()
    {
        if (cable != null)
            cable.transform.DODynamicLookAt(Vector3.zero, .5f, AxisConstraint.Y);
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

            //Material mat = cable.transform.GetChild(0).GetComponent<Material>();
            //mat.SetColor("_MainColor", Color.green);

            cable.transform.GetChild(0).GetComponent<MeshRenderer>().material = cableDone;


            if (GameManager.instance.ejectPlatesActive >= GameManager.instance.NumberOfPlate)
            {
                EjectPlayer();
                CenterManager.instance.DesactivateAllBridge();
                CameraManager.Instance.ChangeCamera();
            }
        }
    }

    public void EjectPlayer()
    {
        CenterPoint.Instance.PointToUIF();

        GameManager GM = GameManager.instance;

        GM.PlayerInMiddle.transform.position = PointAreaManager.instance.GetPlayerRandomPos().position;
        GM.PlayerInMiddle.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
        GM.PlayerInMiddle.GetComponent<Player>().HideGuy(true);

        for (int i = 0; i < GameManager.instance.tabCircle.Count; i++)
        {
            if (GM.tabCircle[i].GetComponentInChildren<Outline>() != null)
            {
                GM.tabCircle[i].GetComponentInChildren<MeshRenderer>().material.color = GameManager.instance.TabMaterialColor[i];
                GM.tabCircle[i].GetComponentInChildren<Outline>().enabled = false;
            }
        }

        GM.ejectPlatesActive = 0;

        foreach (GameObject plate in GM.EjectPlates)
            Destroy(plate);

        GM.EjectPlates.Clear();

        GM.PlayerInMiddle = null;
    }
}
