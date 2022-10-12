using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float normalStrenght;
    [SerializeField] private float specialStrenght;

    [SerializeField] private float range;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float sideRange;

    private PlayerState playerState;

    void Awake()
    {
        playerState = GetComponent<Player>().ActualPlayerState;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        float strenght = normalStrenght;
        if (ctx.started)
            Attack(strenght);
    }
    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        float strenght = specialStrenght;
        if (ctx.started)
            Attack(strenght);
    }

    void Attack(float _strenght)
    {
        GetComponent<PlayerMovement>().animator.SetTrigger("Attack");
        RaycastHit hit;
        
        if (playerState != PlayerState.DEAD && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
        {
            if (hit.transform.tag == "Player")
            {
                hit.rigidbody.AddForce(transform.TransformDirection(Vector3.forward) * _strenght, ForceMode.Impulse);
                Debug.Log(hit.transform.name +  " has been hit");
            }
            else if(hit.transform.tag == "PointArea")
            {
                hit.transform.GetComponent<PointArea>().Dammage(this.gameObject);
            }
        }
        
    }

}
