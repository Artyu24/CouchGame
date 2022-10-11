using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 movementInput;
    [SerializeField] private Quaternion orientation;
    [SerializeField] private float speed;

    [SerializeField] private Vector3 test;

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
        transform.rotation = orientation;

    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector3>();
        if (ctx.performed)
        {
            orientation = quaternion.LookRotation(ctx.ReadValue<Vector3>(), Vector3.up);
        }
    }
}
