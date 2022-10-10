using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float normalStrenght;
    [SerializeField] private float specialStrenght;

    [SerializeField] private float range;
    [SerializeField] private LayerMask layerMask;

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
        {
            hit.rigidbody.AddForce(transform.TransformDirection(Vector3.forward) * _strenght, ForceMode.Impulse);
            Debug.Log("Player has been hit");
        }
    }
}
