using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public int playerID;

    public int score = 0;

    [Header("Variables Game Feel")]

    [Tooltip("Point gagner par le joueur est au milieu")]
    public int scoreMiddle;
    [Tooltip("Temps entre 2 gain de point que le joueur est au milieu")]
    public float middelPointsCooldown;

    private PlayerState actualPlayerState = PlayerState.INIT;
    public PlayerState ActualPlayerState { get => actualPlayerState; set => actualPlayerState = value; }

    public void Kill()
    {
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(GameManager.instance.RespawnDelay);
        actualPlayerState = PlayerState.FIGHTING;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = GameManager.instance.RandomSpawn().position;
    }

    public void HideGuy(bool enable)
    {
        GetComponent<CapsuleCollider>().enabled = enable;
        GetComponentInChildren<MeshRenderer>().enabled = enable;
    }

    private IEnumerator MoreScore()
    {
        yield return new WaitForSeconds(middelPointsCooldown);
        ScoreManager.instance.AddScore(scoreMiddle, this);
        actualPlayerState = PlayerState.FIGHTING;
    }
}
