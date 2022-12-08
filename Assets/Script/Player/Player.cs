using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

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

    #region Component
    private PlayerAttack playerAttack;
    private Rigidbody rb;
    

    #endregion

    //public Gamepad playerManette;

    private void Awake()
    {
        currentColor = GetComponentInChildren<ArrowPlayer>().flecheColor[playerID];
        
        playerAttack = GetComponent<PlayerAttack>();
        rb = GetComponent<Rigidbody>();

        //Debug.Log(currentColor);
        //DG.Tweening.Sequence seq = DOTween.Sequence();
        //seq.Append(graphics.material.DOFade(0.2f, 0.5f));
        //seq.Append(graphics.material.DOFade(1, 0.5f));
        //seq.Play();
    }

    public void Kill()
    {
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {

        if (playerAttack.PlayerHitedBy != null)
        {
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreKill, playerAttack.PlayerHitedBy.GetComponent<Player>());
            playerAttack.PlayerHitedBy = null;
        }
        yield return new WaitForSeconds(GameManager.instance.RespawnDelay);

        CameraManager.Instance.AddPlayerTarget(transform, playerID + 1);

        ActivateRespawnEffect();
    }

    public void ActivateRespawnEffect()
    {
        StartCoroutine(RespawnEffect());
    }

    private IEnumerator RespawnEffect()
    {
        isInvincible = true;
        HideGuy(false);
        Transform pos = PointAreaManager.instance.GetPlayerRandomPos();
        PointAreaManager.instance.DictInUse[pos] = true;
        transform.position = pos.position;

        GameObject effect = Instantiate(ObjectManager.Instance.EffectSpawn, pos.position, Quaternion.identity, transform.parent);
        yield return new WaitForSeconds(2f);

        HideGuy(true);

        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector3.zero;
        actualPlayerState = PlayerState.FIGHTING;

        //Tween a = gameObject.transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.DOColor(new Color(1f, 1f, 1f, 0.2f), 0.5f);
        //Tween b = gameObject.transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.DOColor(new Color(1f, 1f, 1f, 1f), 0.5f);
        //Sequence seq = DOTween.Sequence();
        //seq.Append(a).Append(b).SetLoops(10);

        yield return new WaitForSeconds(2f);
        PointAreaManager.instance.DictInUse[pos] = false;
        Destroy(effect);

        yield return new WaitForSeconds(GameManager.instance.InvincibleDelay);
        isInvincible = false;
    }

    public void HideGuy(bool enable)
    {
        GetComponent<CapsuleCollider>().enabled = enable;
        GetComponent<ChangingColor>().skinedRenderer.enabled = enable;
        rb.useGravity = enable;
        if (!enable)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            playerAttack.EffectSpeBarre.SetActive(false);
            couronne.SetActive(false);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            playerAttack.ActivateEffectSpeBarre();
            if (PlayerManager.instance.playersSortedByScore.Count != 0)
                if (this == PlayerManager.instance.playersSortedByScore[0])
                    couronne.SetActive(true);
        }

        // cacher les particules
    }
}
