using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Material colorMaterial, baseMaterial;
    private Player player;
    private Rigidbody rb;
    public Animator animator;

    private Vector3 movementInput;
    private Quaternion orientation;
    private int actualCircle;
    private float rotation = 0;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Player>().ActualPlayerState == PlayerState.FIGHTING)
        {
            rb.MovePosition(rb.position + movementInput * Time.deltaTime * GameManager.instance.MovementSpeed);
            transform.rotation = orientation;
        }

        if (GetComponent<Player>().ActualPlayerState == PlayerState.MIDDLE)
            GameManager.instance.TabCicle[actualCircle].transform.eulerAngles = new Vector3(0, GameManager.instance.TabCicle[actualCircle].transform.eulerAngles.y + (rotation * GameManager.instance.CircleRotationSpeed * Time.fixedDeltaTime), 0);
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        animator.SetFloat("Magnitude", ctx.ReadValue<Vector3>().sqrMagnitude);
        movementInput = ctx.ReadValue<Vector3>();
        if (ctx.performed && ctx.ReadValue<Vector3>().sqrMagnitude > (GameManager.instance.DeadZoneController * GameManager.instance.DeadZoneController))
        {
            orientation = quaternion.LookRotation(ctx.ReadValue<Vector3>(), Vector3.up);
        }
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        if (player.ActualPlayerState == PlayerState.MIDDLE)
        {
            if (context.performed)
            {
                rotation = context.ReadValue<float>();
            }
            else
                rotation = 0;
        }
    }

    public void OnSwitchCircle(InputAction.CallbackContext context)
    {
        if (player.ActualPlayerState == PlayerState.MIDDLE)
        {
            if (context.started)
            {
                GameManager.instance.TabCicle[actualCircle].GetComponent<Outline>().enabled = false;
                GameManager.instance.TabCicle[actualCircle].GetComponent<MeshRenderer>().material = baseMaterial;

                float nextCircle = context.ReadValue<float>();
                if (actualCircle + nextCircle < 0)
                    actualCircle = GameManager.instance.TabCicle.Length - 1;
                else if (actualCircle + nextCircle > GameManager.instance.TabCicle.Length - 1)
                    actualCircle = 0;
                else
                    actualCircle += (int)nextCircle;

                GameManager.instance.TabCicle[actualCircle].GetComponent<Outline>().enabled = true;
                GameManager.instance.TabCicle[actualCircle].GetComponent<MeshRenderer>().material = colorMaterial;
            }
        }
    }
}
