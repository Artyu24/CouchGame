using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleMovement : MonoBehaviour
{
    private float rotation = 0;

    public float speed = 5;
    private int actualCircle;

    private void FixedUpdate()
    {
        GameManager.instance.TabCicle[actualCircle].transform.eulerAngles = new Vector3(0, GameManager.instance.TabCicle[actualCircle].transform.eulerAngles.y + (rotation * speed * Time.fixedDeltaTime), 0);
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        if (context.performed)
            rotation = context.ReadValue<float>();
        else
            rotation = 0;
    }

    public void OnSwitchCircle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            float nextCircle = context.ReadValue<float>();
            if (actualCircle + nextCircle < 0)
                actualCircle = GameManager.instance.TabCicle.Length - 1;
            else if (actualCircle + nextCircle > GameManager.instance.TabCicle.Length - 1)
                actualCircle = 0;
            else
                actualCircle += (int)nextCircle;
        }
    }
}
