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
    private Player player;
    
    private int actualCircle;
    private float rotation = 0;
    [SerializeField] private float circleSpeed = 5;
    public Color activeColor;
    public Color beginColor;
    private MeshRenderer[] childrenMeshRenderers;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        beginColor = GameManager.instance.TabCicle[actualCircle].GetComponentInChildren<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ActualPlayerState == PlayerState.MIDDLE)
        {
            //input system middle
            childrenMeshRenderers = GameManager.instance.TabCicle[actualCircle].GetComponentsInChildren<MeshRenderer>();
            foreach (var child in childrenMeshRenderers)
            {
                child.material.color = activeColor;
            }
            GameManager.instance.TabCicle[actualCircle].transform.eulerAngles = new Vector3(0,
                GameManager.instance.TabCicle[actualCircle].transform.eulerAngles.y +
                (rotation * speed * Time.fixedDeltaTime), 0);
        }
        else if (player.ActualPlayerState == PlayerState.FIGHTING)
        {
            //input system normal
            rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
            transform.rotation = orientation;

        }
    }
    

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (player.ActualPlayerState == PlayerState.FIGHTING)
        {
            movementInput = ctx.ReadValue<Vector3>();
            if (ctx.performed)
            {
                orientation = quaternion.LookRotation(ctx.ReadValue<Vector3>(), Vector3.up);
            }
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
                childrenMeshRenderers = GameManager.instance.TabCicle[actualCircle].GetComponentsInChildren<MeshRenderer>();
                foreach (var child in childrenMeshRenderers)
                {
                    child.material.color = beginColor;
                }

                float nextCircle = context.ReadValue<float>();
                if (actualCircle + nextCircle < 0)
                    actualCircle = GameManager.instance.TabCicle.Length - 1;
                else if (actualCircle + nextCircle > GameManager.instance.TabCicle.Length - 1)
                    actualCircle = 0;
                else
                    actualCircle += (int) nextCircle;

                childrenMeshRenderers = GameManager.instance.TabCicle[actualCircle].GetComponentsInChildren<MeshRenderer>();
                foreach (var child in childrenMeshRenderers)
                {
                    child.material.color = activeColor;
                }
            }
        }
    }
}
