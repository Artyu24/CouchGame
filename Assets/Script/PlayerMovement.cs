using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 movementInput;
    [SerializeField] private float speed;

    [SerializeField] private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);

    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector3>();

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        float strenght = 5;
        if (ctx.started)
            Attack(strenght);
    }
    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        float strenght = 10;
        if (ctx.started)
            Attack(strenght);
    }

    void Attack(float _strenght)
    {
        Debug.Log("KB = " + _strenght);
    }

}
