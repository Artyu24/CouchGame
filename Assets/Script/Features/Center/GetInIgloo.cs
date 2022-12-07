using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GetInIgloo : MonoBehaviour
{
    private Vector3 offsetCam = Vector3.zero;
    [SerializeField] private GameObject platePref;

    //screen shake, color disque, anim disque (gamefeel) vibration manette, canvas contour
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CenterManager.instance.ActualCenterState == CenterState.ACCESS)
        {
            CameraManager.Instance.ChangeCamera();

            CenterManager.instance.ActualCenterState = CenterState.USE;
            other.GetComponent<Player>().ActualPlayerState = PlayerState.MIDDLE;

            GameObject player = other.gameObject;
            player.transform.DOMove(new Vector3(0, 3, 0), 2);
            StartCoroutine(InitCenterTime(player));
        }
    }

    private IEnumerator InitCenterTime(GameObject player)
    {
        yield return new WaitForSeconds(2.1f);
        InitCenter(player);
        GetComponent<Animator>().SetTrigger("Enter");
    }

    private void InitCenter(GameObject player)
    {
        GameManager GM = GameManager.instance;

        GM.PlayerInMiddle = player.gameObject;

        //On créé les inérupteurs pour faire sortir le joueur
        for (int i = 0; i < GameManager.instance.NumberOfPlate; i++)
        {
            Transform spawnPoint = PointAreaManager.instance.GetRandomPosition();
            GameObject plate = Instantiate(platePref, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
            plate.GetComponent<BoxCollider>().enabled = true;
            GM.EjectPlates.Add(plate);


        }

        //On cache le joueur qui est rentrer dans l'igloo
        player.GetComponent<Player>().HideGuy(false);

        //Ajoute la couleur et la outline au cercle choisis de base
        GM.tabCircle[player.GetComponent<PlayerMovement>().ActualCircle].GetComponent<Outline>().enabled = true;
        GM.tabCircle[player.GetComponent<PlayerMovement>().ActualCircle].GetComponent<MeshRenderer>().material.color = GM.ColorCircleChoose;

        //Gestion du Centre via le Centre Manager
        CenterManager.instance.ActivateAllBridge();

        //pour asdditif, stocker l'offset de la cam (vecteur 3) a chaque update, enlever l'offset, màj l'offset pour le shake et rajouter l'offset après
        StartCoroutine(OnVibrate());
        StartCoroutine(ShakeCam());

    }

    private IEnumerator ShakeCam()
    {
        float timer = 0.0f;
        while (timer < GameManager.instance.shakeDuration)
        {
            GameManager.instance.CameraScene.transform.localPosition -= offsetCam;
            offsetCam = new Vector3(Random.Range(-GameManager.instance.shakePower, GameManager.instance.shakePower), Random.Range(-GameManager.instance.shakePower, GameManager.instance.shakePower), 0);
            GameManager.instance.CameraScene.transform.localPosition += offsetCam;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GameManager.instance.CameraScene.transform.localPosition -= offsetCam;
    }
    public IEnumerator OnVibrate()
    {
        //Debug.Log(PlayerManager.instance.manettes[playerInMid.playerID]);
        //PlayerManager.instance.manettes[playerInMid.playerID].SetMotorSpeeds(0.123f, 0.234f);
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Gamepad.all[i].SetMotorSpeeds(0.123f, 0.234f);
        }
        yield return new WaitForSeconds(2);
        //PlayerManager.instance.manettes[playerInMid.playerID].SetMotorSpeeds(0.0f, 0.0f);
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Gamepad.all[i].SetMotorSpeeds(0.0f, 0.0f);
        }
    }
}
