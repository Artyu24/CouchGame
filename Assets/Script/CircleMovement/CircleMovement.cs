using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleMovement : MonoBehaviour
{
    private float rotation;

    public float speed = 5;
    private int actualCircle;

    private void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + (rotation * speed * Time.fixedDeltaTime), 0);
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
            rotation = context.ReadValue<float>();
    }

    public void OnSwitchCircle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            float nextCircle = context.ReadValue<float>();

        }

    }
}
