using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;

    public int score = 0;
    public int scoreGeneral = 0;

    public bool isChockedWaved = false;

    public bool isInvincible = false;

    public MeshRenderer graphics;

    private PlayerState actualPlayerState = PlayerState.INIT;
    public PlayerState ActualPlayerState { get => actualPlayerState; set => actualPlayerState = value; }

    public Color currentColor;

    public List<GameObject> medals = new List<GameObject>();

    private void Start()
    {
        //DG.Tweening.Sequence seq = DOTween.Sequence();
        graphics = GetComponentInChildren<MeshRenderer>();
        currentColor = graphics.material.color;
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
        //Debug.Log("Invincible");
        isInvincible = true;
        actualPlayerState = PlayerState.FIGHTING;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = PointAreaManager.instance.GetRandomPosition().position;
        //StartCoroutine(InvincibilityFlash());
        yield return new WaitForSeconds(GameManager.instance.InvincibleDelay);
        //Debug.Log(" plus Invincible");
        isInvincible = false;

        if (gameObject.GetComponent<PlayerAttack>().PlayerHitedBy != null)
        {
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreKill, gameObject.GetComponent<PlayerAttack>().PlayerHitedBy.GetComponent<Player>());
            gameObject.GetComponent<PlayerAttack>().PlayerHitedBy = null;
        }
    }

    //public IEnumerator InvincibilityFlash()
    //{
    //    while (isInvincible)
    //    {
    //        graphics.material.color = new Color(1f,1f,1f,0f);
    //        yield return new WaitForSeconds(0.2f);
    //        graphics.material.color = currentColor; 
    //        yield return new WaitForSeconds(0.2f);


    //    }
    //}
    public void HideGuy(bool enable)
    {
        GetComponent<CapsuleCollider>().enabled = enable;
        GetComponentInChildren<MeshRenderer>().enabled = enable;
        GetComponent<Rigidbody>().useGravity = enable;
    }
}
