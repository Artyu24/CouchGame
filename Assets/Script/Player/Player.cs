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


    [Header("Variables Game Feel")]

    private PlayerState actualPlayerState = PlayerState.INIT;
    public PlayerState ActualPlayerState { get => actualPlayerState; set => actualPlayerState = value; }





    public void Kill()
    {
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(GameManager.instance.RespawnDelay);
        Debug.Log("Invincible");
        isInvincible = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        //appelle fonction invincible
        actualPlayerState = PlayerState.FIGHTING;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = GameManager.instance.RandomSpawn().position;
        yield return new WaitForSeconds(GameManager.instance.InvincibleDelay);
        Debug.Log(" plus Invincible");
        isInvincible = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;



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
    }
}
