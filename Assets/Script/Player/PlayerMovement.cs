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
    private Player player;
    private Rigidbody rb;
    public Animator animator;

    private Vector3 movementInput;
    private Quaternion orientation;
    private float rotation = 0;
    private int actualCircle;
    public int ActualCircle => actualCircle;

    bool isInteracting = false;


    public GameObject meteorite;
    public GameObject departChoc;
    public GameObject departMeteorite;
    public GameObject chocWave;
    public GameObject chocWaveSprite;

    private float movementSpeed;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    //public static PlayerMovement instance;


    // Start is called before the first frame update
    void Awake()
    {
        //if (instance == null)
        //        instance = this;
            rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();

        MovementSpeed = GameManager.instance.MaxMovementSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Player>().ActualPlayerState == PlayerState.FIGHTING)
        {
            rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * MovementSpeed);
            transform.rotation = orientation;
        }

        if (GetComponent<Player>().ActualPlayerState == PlayerState.MIDDLE)
            GameManager.instance.TabCircle[actualCircle].transform.eulerAngles = new Vector3(0, GameManager.instance.TabCircle[actualCircle].transform.eulerAngles.y + (rotation * GameManager.instance.CircleRotationSpeed * Time.fixedDeltaTime), 0);

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
                GameManager.instance.TabCircle[actualCircle].GetComponent<Outline>().enabled = false;
                GameManager.instance.TabCircle[actualCircle].GetComponent<MeshRenderer>().material.color = GameManager.instance.TabMaterialColor[actualCircle];

                float nextCircle = context.ReadValue<float>();
                if (actualCircle + nextCircle < 0)
                    actualCircle = GameManager.instance.TabCircle.Count - 1;
                else if (actualCircle + nextCircle > GameManager.instance.TabCircle.Count - 1)
                    actualCircle = 0;
                else
                    actualCircle += (int)nextCircle;

                GameManager.instance.TabCircle[actualCircle].GetComponent<Outline>().enabled = true;
                GameManager.instance.TabCircle[actualCircle].GetComponent<MeshRenderer>().material.color = GameManager.instance.ColorCircleChoose;
            }
        }
    }
    
    public void Meteo1(InputAction.CallbackContext context) 
    {
        OnMeteorite(context,1);
    
    }
    public void Meteo2(InputAction.CallbackContext context)
    {
        OnMeteorite(context, 2);

    }
    public void Meteo3(InputAction.CallbackContext context)
    {
        OnMeteorite(context, 3);

    }
    public void Meteo4(InputAction.CallbackContext context)
    {
        OnMeteorite(context, 4);

    }

    public void OnMeteorite(InputAction.CallbackContext context, int i)
    {
        if (player.ActualPlayerState == PlayerState.MIDDLE)
        {
            if (context.performed)
            {
                if (isInteracting == false)
                {
                    GameObject metoto = Instantiate(meteorite, departMeteorite.transform.position, departMeteorite.transform.rotation);
                    metoto.GetComponent<MeteorMovement>().MovePlanete(i);
                    StartCoroutine(CooldownForInteraction());
                }
            }
            
        }
    }
    public void OnChocWave(InputAction.CallbackContext context)
    {
        if (player.ActualPlayerState == PlayerState.MIDDLE)
        {
            if (context.performed)
            {
                if (isInteracting == false)
                {
                    Instantiate(chocWave, departChoc.transform.position, departChoc.transform.rotation);
                    //Instantiate(chocWaveSprite, departChoc.transform.position, departChoc.transform.rotation);
                    StartCoroutine(CooldownForInteraction());
                }
            }

        }
    }

    public IEnumerator CooldownForInteraction()
    {
        isInteracting = true;
        yield return new WaitForSeconds(GameManager.instance.InteractionCD);
        isInteracting = false;
    }
}
