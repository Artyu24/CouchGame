using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointArea : MonoBehaviour
{
    [SerializeField] public int hp;
    [SerializeField] private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Dammage(GameObject player)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        hp--;
        Debug.Log(player.name + " SCORE ++");
        animator.SetTrigger("SHAKEMETHAT");
    }
}
