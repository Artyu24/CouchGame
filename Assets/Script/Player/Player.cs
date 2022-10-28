using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;

    public int score = 0;
    public int scoreGeneral = 0;

    public bool isChockedWaved = false;

    public bool isInvincible = false;

    public MeshRenderer graphics;

    [Header("Variables Game Feel")]

    private PlayerState actualPlayerState = PlayerState.INIT;
    public PlayerState ActualPlayerState { get => actualPlayerState; set => actualPlayerState = value; }
    private void Start()
    {
        //DG.Tweening.Sequence seq = DOTween.Sequence();
        //graphics = GetComponentInChildren<MeshRenderer>();
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
        yield return new WaitForSeconds(GameManager.instance.RespawnDelay);
        Debug.Log("Invincible");
        isInvincible = true;
        actualPlayerState = PlayerState.FIGHTING;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = PointAreaManager.instance.RandomPosition().position;
        while (isInvincible)
        {
            graphics.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            graphics.material.color = Color.green;
            yield return new WaitForSeconds(0.2f);


        }
        yield return new WaitForSeconds(GameManager.instance.InvincibleDelay);
        Debug.Log(" plus Invincible");
        isInvincible = false;

        if (gameObject.GetComponent<PlayerAttack>().PlayerHitedBy != null)
        {
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreKill, gameObject.GetComponent<PlayerAttack>().PlayerHitedBy.GetComponent<Player>());
            gameObject.GetComponent<PlayerAttack>().PlayerHitedBy = null;
        }
    }


    public void HideGuy(bool enable)
    {
        GetComponent<CapsuleCollider>().enabled = enable;
        GetComponentInChildren<MeshRenderer>().enabled = enable;
        GetComponent<Rigidbody>().useGravity = enable;
    }
}
