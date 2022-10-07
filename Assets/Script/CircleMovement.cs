using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleMovement : MonoBehaviour
{
    private Vector3 movementInput;

    private float rotation;
    public bool test;

    private Rigidbody rb;

    public float speed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Debug.Log(rotation);

        rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + (rotation * speed * Time.fixedDeltaTime), 0);
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
            test = true;
        else if (context.canceled)
            test = false;

        rotation = context.ReadValue<float>();
    }
}
