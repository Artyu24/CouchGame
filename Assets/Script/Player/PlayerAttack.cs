using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    #region Variable
    [Header("Variable")]
    [SerializeField] private bool canAttack = true;
    public int maxSpecial = 20;
    private int currentSpecial;
    public int CurrentSpecial
    {
        get => currentSpecial;
        set => currentSpecial = value;
    }
    private GameObject playerHitedBy;
    public GameObject PlayerHitedBy
    {
        get => playerHitedBy;
        set => playerHitedBy = value;
    }

    private Player player;

    private GameObject playerHit;

    private Slider speBarreSlider;
    public Gradient speBarreGradient;
    public Slider SpeBarreSlider { get => speBarreSlider; set => speBarreSlider = value; }
    private GameObject effectSpeBarre;

    [SerializeField] public bool bumperIsCharged;
    public bool BumperIsCharged { get => bumperIsCharged; set => bumperIsCharged = value; }

    [Header("Range")]
    [SerializeField] private LayerMask layerMask;
    [Tooltip("La partie sur le cot� en Degr� (prendre en compte x2 pour l'amplitude total)")]
    private float middleDirAngle;

    private void Start()
    {
        effectSpeBarre = transform.GetChild(1).gameObject;
        effectSpeBarre.SetActive(false);
        player = GetComponent<Player>();
        playerHit = Resources.Load<GameObject>("Features/Hit");
    }
    #endregion

    #region InputSysteme
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (player != null)
        {
            if ((player.ActualPlayerState == PlayerState.FIGHTING || player.ActualPlayerState == PlayerState.FLYING) && GameManager.instance.ActualGameState == GameState.INGAME)
            {
                float strenght = GameManager.instance.NormalStrenght;
                if (ctx.started && canAttack && GameManager.instance.PlayerInMiddle != this.gameObject)
                {
                    int xcount = Random.Range(0, 5);
                    FindObjectOfType<AudioManager>().PlayRandom(SoundState.EffortSound);
                    FindObjectOfType<AudioManager>().PlayRandom(SoundState.NormalPunch);
                    StartCoroutine(AttackCoroutine(strenght, transform.GetChild(3).gameObject));
                }
            }
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        if (player != null)
        {
            if ((player.ActualPlayerState == PlayerState.FIGHTING || player.ActualPlayerState == PlayerState.FLYING) && GameManager.instance.ActualGameState == GameState.INGAME)
            {
                float strenght = GameManager.instance.SpecialStrenght;
                if (ctx.started && canAttack && currentSpecial == maxSpecial && GameManager.instance.PlayerInMiddle != this.gameObject)
                {
                    bumperIsCharged = true;
                    effectSpeBarre.SetActive(false);
                    currentSpecial = 0;
                    speBarreSlider.value = currentSpecial;
                    int xcount = Random.Range(0, 3);
                    StartCoroutine(AttackCoroutine(strenght, transform.GetChild(4).gameObject));
                    FindObjectOfType<AudioManager>().PlayRandom(SoundState.PunchSpecialSound);
                    FindObjectOfType<AudioManager>().PlayRandom(SoundState.SpecialPunchHit);
                }
            }
        }
    }
    #endregion

    #region Attack
    IEnumerator AttackCoroutine(float _strenght, GameObject _poisson)
    {
        canAttack = false;
        Attack(_strenght);

        _poisson.GetComponent<Animator>().SetTrigger("Smash");

        yield return new WaitForSecondsRealtime(GameManager.instance.AttackCd);
        canAttack = true;
        // Ici on mange des gauffres, h� ouai
    }


    void Attack(float _strenght)
    {
        if (player.ActualPlayerState != PlayerState.DEAD)
        {
            GetComponent<PlayerMovement>().animator.SetTrigger("Attack");
            #region Range
            float it = -GameManager.instance.SideRangeDeg;
            for (int i = 0; i < GameManager.instance.SideRangeDeg * 2; i++)//do all the raycast
            {
                RaycastHit hit;
                #region Raycast Calcul
                //next 5 lines calcul the right angle and do the raycast
                middleDirAngle = Mathf.Atan2(transform.TransformDirection(Vector3.forward).z, transform.TransformDirection(Vector3.forward).x);//si �a marche plus faut faire le transform.TransformDirection apr�s les calcules
                float angle = middleDirAngle - Mathf.Deg2Rad * it;
                Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                Debug.DrawRay((new Vector3(dir.x / 3f,0,dir.z / 3f) + transform.localPosition), dir * GameManager.instance.Range, Color.blue, 5.0f);
                Physics.Raycast((new Vector3(dir.x / 3, 0, dir.z / 3) + transform.localPosition), dir, out hit, GameManager.instance.Range,layerMask);                
                #endregion

                #region HitCondition

                if (hit.transform != null)
                {
                    if (hit.transform.tag == "Player")//if we hit a player we push him
                    {
                        //Vector3 hitDir = new Vector3(hit.transform.position.x - transform.position.x, 0, hit.transform.position.z - transform.position.z);
                        hit.rigidbody.AddForce(new Vector3(dir.x, 1, dir.z) * _strenght, ForceMode.Impulse);
                        //Debug.Log(hit.transform.name + " has been hit");
                        hit.transform.GetComponent<PlayerAttack>().HitTag(gameObject);
                        hit.transform.GetComponent<PlayerMovement>().animator.SetTrigger("Hit");
                        hit.transform.GetComponent<Player>().ActualPlayerState = PlayerState.FLYING;
                        HitParticle(hit.point);
                        FindObjectOfType<AudioManager>().PlayRandom(SoundState.HitSound);
                        return;
                    }
                    if (hit.transform.tag == "FishBag")//if we hit a FishBag we do things
                    {
                        //Debug.Log(hit.transform.GetComponent<FishBag>().isGolden);
                        hit.transform.GetComponent<FishBag>().Damage(gameObject);
                        HitParticle(hit.point);
                        return;
                    }
                    if (hit.transform.tag == "Bomb")// if we hit a bomb it push it and trigger it
                    {
                        hit.transform.GetComponentInParent<Rigidbody>().mass = 1;
                        hit.rigidbody.AddForce(new Vector3(dir.x, 1, dir.z) * _strenght, ForceMode.Impulse);
                        hit.transform.GetComponent<Bomb>().isGrounded = false;
                        hit.transform.GetComponent<IInteractable>().Interact(GetComponent<Player>());
                        HitParticle(hit.point);
                        return;
                    }
                    if (hit.transform.tag == "Bumper")// if we hit a bumper it push it and trigger it
                    {
                        hit.transform.GetComponent<RailedBumper>().strenghtPlayerAttack = _strenght;
                    }

                        if (hit.transform.GetComponent<IInteractable>() != null)
                    {
                        hit.transform.GetComponent<IInteractable>().Interact(GetComponent<Player>());
                        HitParticle(hit.point); 
                        return;
                    }

                }
                #endregion

                #region Don't Do To Much
                if (it < GameManager.instance.SideRangeDeg)
                    it += GameManager.instance.SideRangeDeg / GameManager.instance.SideRangeDeg * 4;
                else
                    break;
                int xcount = Random.Range(0, 5);
                FindObjectOfType<AudioManager>().PlayRandom(SoundState.HurtSound);
                
                #endregion

            }
            #endregion
        }
    }
    #endregion

    private void HitParticle(Vector3 _pos)
    {
        GameObject PS = Instantiate(playerHit, _pos, Quaternion.identity);
        Destroy(PS, 2f);
    }

    public void AddSpeBarrePoint(int _point)
    {
        currentSpecial += _point;
        speBarreSlider.value = currentSpecial;

        if(currentSpecial >= maxSpecial && !effectSpeBarre.activeInHierarchy)
        {
            effectSpeBarre.SetActive(true);
            //SpecialIsCharged

        }
    }

    public void HitTag(GameObject _player)
    {
        StartCoroutine(WhoHitMe(_player, 10));
    }

    IEnumerator WhoHitMe(GameObject _player, float _s)
    {
        playerHitedBy = _player;
        yield return new WaitForSecondsRealtime(_s);
        playerHitedBy = null;
    }
}
