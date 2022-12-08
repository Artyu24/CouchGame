using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int playerID;

    public int score = 0;
    public int scoreGeneral = 0;

    #region Boost / Malus
    private bool multiplier = false;
    private bool isSpeedUp = false;
    private bool isSlow = false;
    public bool Multiplier { get => multiplier; set => multiplier = value; }
    public bool IsSpeedUp { get => isSpeedUp; set => isSpeedUp = value; }
    public bool IsSlow { get => isSlow; set => isSlow = value; }

    public Coroutine speedCoroutine;
    public Coroutine multiplierCoroutine;
    #endregion

    public bool isChockedWaved = false;
    public bool isInvincible = false;

    private PlayerState actualPlayerState = PlayerState.INIT;
    public PlayerState ActualPlayerState { get => actualPlayerState; set => actualPlayerState = value; }

    public Color currentColor;

    public List<GameObject> medals = new List<GameObject>();
    public GameObject couronne;

    //public Gamepad playerManette;

    private void Awake()
    {
        currentColor = GetComponentInChildren<ArrowPlayer>().flecheColor[playerID];
        //Debug.Log(currentColor);
        //DG.Tweening.Sequence seq = DOTween.Sequence();
        //seq.Append(graphics.material.DOFade(0.2f, 0.5f));
        //seq.Append(graphics.material.DOFade(1, 0.5f));
        //seq.Play();
    }

    private void Start()
    {/*
        InitCam();*/
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode loadSceneMode) => 
        {
            InitCam();
        };
    }

    private void InitCam()
    {
        if (SceneManager.GetActiveScene().name != "Leaderboard")
        {
            GetComponent<ArrowPlayer>().cam = GameManager.instance.CameraScene;
        }
        CameraManager.Instance.AddPlayerTarget(transform, (playerID+1));
    }

    public void Kill()
    {
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {

        if (gameObject.GetComponent<PlayerAttack>().PlayerHitedBy != null)
        {
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreKill, gameObject.GetComponent<PlayerAttack>().PlayerHitedBy.GetComponent<Player>());
            gameObject.GetComponent<PlayerAttack>().PlayerHitedBy = null;
        }
        yield return new WaitForSeconds(GameManager.instance.RespawnDelay);

        isInvincible = true;
        actualPlayerState = PlayerState.FIGHTING;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = PointAreaManager.instance.GetPlayerRandomPos().position;
        CameraManager.Instance.AddPlayerTarget(transform, playerID + 1);

        Tween a = gameObject.transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.DOColor(new Color(1f, 1f, 1f, 0.2f), 0.5f);
        Tween b = gameObject.transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.DOColor(new Color(1f, 1f, 1f, 1f), 0.5f);
        Sequence seq = DOTween.Sequence();
        seq.Append(a).Append(b).SetLoops(10);

        yield return new WaitForSeconds(GameManager.instance.InvincibleDelay);
        isInvincible = false;

    }

    public void HideGuy(bool enable)
    {
        GetComponent<CapsuleCollider>().enabled = enable;
        GetComponent<ChangingColor>().skinedRenderer.enabled = enable;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = enable;
        if(!enable)
            rb.constraints = RigidbodyConstraints.FreezeAll;
        else
            rb.constraints = RigidbodyConstraints.FreezeRotation;

        //rb.transform.GetChild(0).GetChild(1).GetChild(3).gameObject.SetActive(false);
        // cacher les particules
    }
}
