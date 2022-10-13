using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GetInIgloo : MonoBehaviour
{
    private Vector3 offsetCam = Vector3.zero;
    [Tooltip("Pas touche mdr")]
    [SerializeField] private Camera cameraScene;

    [Header("Variables Game Feel")]
    [Tooltip("Variable pour augmenter ou diminuer le shake de la cam")]
    public float shakePower = 0.05f;
    [Tooltip("Variable pour augmenter ou diminuer le temps du shake de la cam")]
    public float shakeDuration = 0.5f;

    //screen shake, color disque, anim disque (gamefeel) vibration manette, canvas contour
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.PlayerInMiddle == null)
        {
            GameManager.instance.PlayerInMiddle = other.gameObject;
            for (int i = 0; i < GameManager.instance.EjectPlates.Length; i++)
            {
                GameManager.instance.EjectPlates[i].SetActive(true);
            }
            other.GetComponent<Player>().ActualPlayerState = PlayerState.MIDDLE;
            Debug.Log("PlayerState : " + other.GetComponent<Player>().ActualPlayerState);

            other.GetComponent<Player>().HideGuy(false);

            //pour asdditif, stocker l'offset de la cam (vecteur 3) a chaque update, enlever l'offset, màj l'offset pour le shake et rajouter l'offset après
            OnVibrate();
            StartCoroutine(ShakeCam());
        }
    }

    private IEnumerator ShakeCam()
    {
        float timer = 0.0f;
        while (timer < shakeDuration)
        {
            cameraScene.transform.localPosition -= offsetCam;
            offsetCam = new Vector3(Random.Range(-shakePower, shakePower), Random.Range(-shakePower, shakePower), 0);
            cameraScene.transform.localPosition += offsetCam;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();  
        }
        cameraScene.transform.localPosition -= offsetCam;
        Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
    }
    public void OnVibrate()
    {
        Gamepad.current.SetMotorSpeeds(0.123f, 0.234f);
    }
}
