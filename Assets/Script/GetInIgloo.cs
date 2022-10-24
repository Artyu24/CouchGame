using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GetInIgloo : MonoBehaviour
{
    private Vector3 offsetCam = Vector3.zero;

    //screen shake, color disque, anim disque (gamefeel) vibration manette, canvas contour
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.PlayerInMiddle == null)
        {
            GameManager.instance.PlayerInMiddle = other.gameObject;

            for (int i = 0; i < GameManager.instance.EjectPlates.Length; i++)
            {
                GameManager.instance.EjectPlates[i].SetActive(true);
                GameManager.instance.EjectPlates[i].GetComponentInChildren<MeshRenderer>().material.color = GameManager.instance.ActiveColor;
                GameManager.instance.EjectPlates[i].GetComponent<BoxCollider>().enabled = true;
            }

            other.GetComponent<Player>().ActualPlayerState = PlayerState.MIDDLE;
            other.GetComponent<Player>().HideGuy(false);

            GameManager.instance.TabCircle[other.GetComponent<PlayerMovement>().ActualCircle].GetComponent<Outline>().enabled = true;
            GameManager.instance.TabCircle[other.GetComponent<PlayerMovement>().ActualCircle].GetComponent<MeshRenderer>().material.color = GameManager.instance.ColorCircleChoose;

            //pour asdditif, stocker l'offset de la cam (vecteur 3) a chaque update, enlever l'offset, màj l'offset pour le shake et rajouter l'offset après
            OnVibrate();
            StartCoroutine(ShakeCam());
        }
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
        foreach (var gamepad in GameManager.instance.manettes)
        {
            gamepad.SetMotorSpeeds(0.0f, 0.0f);
        }
    }
    public void OnVibrate()
    {
        Gamepad.current.SetMotorSpeeds(0.123f, 0.234f);
    }
}
