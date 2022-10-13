using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointArea : MonoBehaviour
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

        Debug.Log(player.name + " SCORE ++");
        animator.SetTrigger("SHAKEMETHAT");

        if (hp <= 0)
        {
            Destroy(gameObject);
            PointAreaManager.instance.Spawn();

        }
    }
}
