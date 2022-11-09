using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBag : MonoBehaviour
{
    [SerializeField] public int hp = 10;
    [SerializeField] private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Damage(GameObject player)
    {
        transform.localScale -= new Vector3(transform.localScale.x - (transform.localScale.x/10), transform.localScale.x - (transform.localScale.y / 10), transform.localScale.z - (transform.localScale.x / 10));
        hp--;

        ScoreManager.instance.AddScore(ScoreManager.instance.scorePointArea, player.GetComponent<Player>());

        if (player.GetComponent<PlayerAttack>().CurrentSpecial < player.GetComponent<PlayerAttack>().maxSpecial)
            player.GetComponent<PlayerAttack>().AddSpeBarrePoint();

        animator.SetTrigger("SHAKEMETHAT");

        if (hp <= 0)
        {
            Destroy(gameObject);
            PointAreaManager.instance.StartNextSpawn();
        }
    }
}