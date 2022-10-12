using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GetInIgloo : MonoBehaviour
{
    private Vector3 offsetCam = Vector3.zero;
    [SerializeField] private Camera camera;
    //[SerializeField] private GameObject redBorder;
    [Header("Variables Game Feel")]
    public float shakePower = 0.05f;

    public float shakeDuration = 0.5f;

    //screen shake, color disque, anim disque (gamefeel) vibration manette, canvas contour
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerInMiddle = other.gameObject;
            other.GetComponent<Player>().ActualPlayerState = PlayerState.MIDDLE;
            Debug.Log("PlayerState : " + other.GetComponent<Player>().ActualPlayerState);
            
            //pour asdditif, stocker l'offset de la cam (vecteur 3) a chaque update, enlever l'offset, m�j l'offset pour le shake et rajouter l'offset apr�s
            OnVibrate();
            StartCoroutine(ShakeCam());
        }
    }

    private IEnumerator ShakeCam()
    {
        float timer = 0.0f;
        //redBorder.SetActive(true);
        while (timer < shakeDuration)
        {
            camera.transform.localPosition -= offsetCam;
            offsetCam = new Vector3(Random.Range(-shakePower, shakePower), Random.Range(-shakePower, shakePower), 0);
            camera.transform.localPosition += offsetCam;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();  
        }
        camera.transform.localPosition -= offsetCam;
        Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        //redBorder.SetActive(false);

    }
    public void OnVibrate()
    {
        Gamepad.current.SetMotorSpeeds(0.123f, 0.234f);
    }
}
