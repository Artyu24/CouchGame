using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private bool switchDone = false;
    private int actualCircle;
    public int ActualCircle => actualCircle;

    bool isInteracting = false;
    
    private float speed;
    public float Speed { get => speed; set => speed = value; }


    private GameObject meteorite;
    private Vector3 departChoc = Vector3.zero;
    private GameObject chocWave;




    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();

        speed = GameManager.instance.MoveSpeed;

        meteorite = Resources.Load<GameObject>("Meteorite");
        chocWave = Resources.Load<GameObject>("ChocWave");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.ActualGameState == GameState.INGAME || GameManager.instance.ActualGameState == GameState.LOBBY)
        {
            if (player.ActualPlayerState == PlayerState.FIGHTING)
            {
                rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
                transform.rotation = orientation;
            }

            if (player.ActualPlayerState == PlayerState.FLYING)
            {
                StartCoroutine(isFlying());   
                Debug.DrawRay(transform.position, Vector3.down * transform.localScale.y, Color.red, 10f);
            }

            if (player.ActualPlayerState == PlayerState.MIDDLE)
                GameManager.instance.tabCircle[actualCircle].transform.eulerAngles = new Vector3(0, GameManager.instance.tabCircle[actualCircle].transform.eulerAngles.y + (rotation * GameManager.instance.CircleRotationSpeed * Time.fixedDeltaTime), 0);
        }
    }

    void Update()
    {
        transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
        transform.GetChild(0).localPosition = new Vector3(0, -0.9f, 0);
        transform.GetChild(3).localPosition -= movementInput;
        //Debug.Log(transform.GetChild(0).name);
    }

    IEnumerator isFlying()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y + 0.1f);
        if (isGrounded)
        {
            if(player.ActualPlayerState != PlayerState.MIDDLE)
                player.ActualPlayerState = PlayerState.FIGHTING;
        }
        else
        {
            yield return new WaitForSecondsRealtime(2.0f);
            if (player.ActualPlayerState != PlayerState.MIDDLE)
                player.ActualPlayerState = PlayerState.FIGHTING;
        }
    }

    #region INPUT

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector3>();

        if (player.ActualPlayerState == PlayerState.FIGHTING && GameManager.instance.ActualGameState == GameState.INGAME || GameManager.instance.ActualGameState == GameState.LOBBY)
        {
            if (ctx.performed && ctx.ReadValue<Vector3>().sqrMagnitude > (GameManager.instance.DeadZoneController * GameManager.instance.DeadZoneController))
            {
                orientation = quaternion.LookRotation(ctx.ReadValue<Vector3>(), Vector3.up);
            }

            if(movementInput == Vector3.zero)
                animator.SetBool("Run", false);
            else
                animator.SetBool("Run", true);
        }
    }

    public void OnCircleMovement(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector3>();

        if (player.ActualPlayerState == PlayerState.MIDDLE && GameManager.instance.ActualGameState == GameState.INGAME)
        {
            rotation = 0;

            if (Math.Abs(movementInput.x) > Math.Abs(movementInput.z))
            {
                //Rotation
                if (Math.Abs(movementInput.x) > 0.9f && Math.Abs(movementInput.z) <= 0.2f)
                {
                    if (ctx.performed)
                    {
                        if (movementInput.x < 0)
                            rotation = -1;
                        else
                            rotation = 1;

                        //son rota

                    }
                }
            }
            else
            {
                //Switch
                if (Math.Abs(movementInput.x) <= 0.2f && Math.Abs(movementInput.z) > 0.9f)
                {
                    if (!switchDone)
                    {
                        switchDone = true;

                        if (GameManager.instance.tabCircle[actualCircle].GetComponentInChildren<Outline>())
                        {
                            GameManager.instance.tabCircle[actualCircle].GetComponentInChildren<Outline>().enabled = false;
                            GameManager.instance.tabCircle[actualCircle].GetComponentInChildren<MeshRenderer>().material.color = GameManager.instance.TabMaterialColor[actualCircle];
                        }

                        float nextCircle = 0;
                        if (movementInput.z < 0)
                            nextCircle = -1;
                        else
                            nextCircle = 1;

                        if (actualCircle + nextCircle < 0)
                            actualCircle = GameManager.instance.tabCircle.Count - 1;
                        else if (actualCircle + nextCircle > GameManager.instance.tabCircle.Count - 1)
                            actualCircle = 0;
                        else
                            actualCircle += (int)nextCircle;

                        if (GameManager.instance.tabCircle[actualCircle].GetComponentInChildren<Outline>())
                        {
                            GameManager.instance.tabCircle[actualCircle].GetComponentInChildren<Outline>().enabled = true;
                            GameManager.instance.tabCircle[actualCircle].GetComponentInChildren<MeshRenderer>().material.color = GameManager.instance.ColorCircleChoose;
                        }
                        //son switch plata
                        FindObjectOfType<AudioManager>().PlayRandom(SoundState.SwitchCircleSound);

                    }
                }
                else
                    switchDone = false;
            }
        }
    }

    public void OnChocWave(InputAction.CallbackContext context)
    {
        if (player.ActualPlayerState == PlayerState.MIDDLE && GameManager.instance.ActualGameState == GameState.INGAME)
        {
            if (context.performed)
            {
                if (isInteracting == false)
                {
                    GameManager.instance.ButtonToPress.SetActive(false);
                    Instantiate(chocWave, departChoc , quaternion.identity);
                    //Instantiate(chocWaveSprite, departChoc.transform.position, departChoc.transform.rotation);
                    StartCoroutine(CooldownForInteraction());
                }
            }

        }
    }
    #endregion

    #region Interaction Object

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<IPickable>() != null)
            col.GetComponent<IPickable>().Interact(player);    
    }

    public IEnumerator CooldownForInteraction()
    {
        isInteracting = true;
        yield return new WaitForSeconds(GameManager.instance.InteractionCD);
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.RechargedShockwaveSound);
        if(GameManager.instance.PlayerInMiddle != null)
        {

            GameManager.instance.ButtonToPress.SetActive(true);
        }
        ////// llalalalalalalalalalalalalalalalalalal
        isInteracting = false;
    }

    public void ChangeSpeed()
    {
        if (player.IsSlow)
        {
            speed = GameManager.instance.MinMoveSpeed;
            animator.SetFloat("RunModifier", .5f);
        }
        else if(player.IsSpeedUp)
        {
            speed = GameManager.instance.MaxMoveSpeed;
            animator.SetFloat("RunModifier", 2f);
        }
        else
        {
            speed = GameManager.instance.MoveSpeed;
            animator.SetFloat("RunModifier", 1f);
        }
    }

    #endregion

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * (transform.localScale.y + 0.1f));
    }
}
