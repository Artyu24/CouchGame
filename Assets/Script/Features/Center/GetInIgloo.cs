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
    private Animator anim;

    private void Start()
    {
        CenterManager.instance.centerLight = transform.GetChild(transform.childCount - 3).gameObject;
    }

    //screen shake, color disque, anim disque (gamefeel) vibration manette, canvas contour
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CenterManager.instance.ActualCenterState == CenterState.ACCESS)
        {
            CameraManager.Instance.ChangeCamera();

            CenterManager.instance.ActualCenterState = CenterState.USE;
            other.GetComponent<Player>().ActualPlayerState = PlayerState.MIDDLE;

            Debug.Log(transform.GetChild(transform.childCount - 3).name);

            CenterPoint.Instance.SetUp(other.GetComponent<Player>());

            GameObject player = other.gameObject;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            player.transform.DOMove(new Vector3(0, 3, 0), 2).onComplete += () =>
            {
                InitCenter(player);
                GetComponent<Animator>().SetTrigger("Enter");
                FindObjectOfType<AudioManager>().PlayRandom(SoundState.TakeIglooControlSound);
                FindObjectOfType<AudioManager>().PlayRandom(SoundState.SatisfySound);
                CenterManager.instance.centerLight.GetComponent<Light>().color = Color.red; //set the light in red when getting in igloo

            };
        }
    }

    private void InitCenter(GameObject player)
    {
        //On cache le joueur qui est rentrer dans l'igloo
        player.transform.DOMove(new Vector3(0, 0, 0), 0.05f).SetEase(Ease.Linear).onComplete += () =>
        {

            player.transform.position = Vector3.zero;
            player.GetComponent<Player>().HideGuy(false);
            StartCoroutine(GameManager.instance.CircleWaveEffect());
        };

        GameManager GM = GameManager.instance;

        GameObject _pipe = Resources.Load<GameObject>("Features/CableShield");

        GM.PlayerInMiddle = player.gameObject;

        //On créé les inérupteurs pour faire sortir le joueur
        for (int i = 0; i < GameManager.instance.NumberOfPlate; i++)
        {
            Transform spawnPoint = PointAreaManager.instance.GetRandomPosition();
            GameObject plate = Instantiate(platePref, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
            plate.GetComponent<BoxCollider>().enabled = true;
            GM.EjectPlates.Add(plate);

            Vector3 _pos = Vector3.Lerp(transform.position, plate.transform.position, 0.5f);
            GameObject p = Instantiate(_pipe, _pos, Quaternion.identity);
            p.transform.parent = plate.transform;
            plate.transform.GetComponent<EjectPlayerCentre>().cable = p;
        }


        //Ajoute la couleur et la outline au cercle choisis de base
        if (GM.tabCircle[player.GetComponent<PlayerMovement>().ActualCircle].GetComponentInChildren<Outline>() != null)
        {
            GM.tabCircle[player.GetComponent<PlayerMovement>().ActualCircle].GetComponentInChildren<Outline>().enabled = true;
            GM.tabCircle[player.GetComponent<PlayerMovement>().ActualCircle].GetComponentInChildren<MeshRenderer>().material.color = GM.ColorCircleChoose;
        }

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
