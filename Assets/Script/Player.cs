using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerID;
    public float respawnDelay = 5;
    public Transform spawnTransform;

    private Rigidbody rb;

    private PlayerState actualPlayerState = PlayerState.INIT;
    public PlayerState ActualPlayerState { get => actualPlayerState; set => actualPlayerState = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Kill()
    {
        StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        transform.position = spawnTransform.position;
    }
}
