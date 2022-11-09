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

    private Slider speBarreSlider;
    public Slider SpeBarreSlider { get => speBarreSlider; set => speBarreSlider = value; }

    [Header("Range")]
    [SerializeField] private LayerMask layerMask;
    [Tooltip("La partie sur le coté en Degré (prendre en compte x2 pour l'amplitude total)")]
    private float middleDirAngle;

    #endregion

    #region InputSysteme
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        float strenght = GameManager.instance.NormalStrenght;
        if (ctx.started && canAttack)
        {
            int xcount = Random.Range(0, 5);

            switch (xcount)
            {
                case 0:
                    FindObjectOfType<AudioManager>().Play("Effort1");
                    break;
                case 1:
                    FindObjectOfType<AudioManager>().Play("Effort2");
                    break;
                case 2:
                    FindObjectOfType<AudioManager>().Play("Effort3");
                    break;
                case 3:
                    FindObjectOfType<AudioManager>().Play("Effort4");
                    break;
                case 4:
                    FindObjectOfType<AudioManager>().Play("Effort5");
                    break;
                case 5:
                    FindObjectOfType<AudioManager>().Play("Effort6");
                    break;
            }
            StartCoroutine(AttackCoroutine(strenght));
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        float strenght = GameManager.instance.SpecialStrenght;
        if (ctx.started && canAttack && currentSpecial == maxSpecial)
        {
            currentSpecial = 0;
            speBarreSlider.value = currentSpecial;
            int xcount = Random.Range(0, 3);

            switch (xcount)
            {
                case 0:
                    FindObjectOfType<AudioManager>().Play("Special1");
                    break;
                case 1:
                    FindObjectOfType<AudioManager>().Play("Special2");
                    break;
                case 2:
                    FindObjectOfType<AudioManager>().Play("Special3");
                    break;
                case 3:
                    FindObjectOfType<AudioManager>().Play("Special4");
                    break;

            }
            StartCoroutine(AttackCoroutine(strenght));
        }
    }
    #endregion

    
    #region Attack
    IEnumerator AttackCoroutine(float _strenght)
    {
        canAttack = false;
        Attack(_strenght);

        yield return new WaitForSecondsRealtime(GameManager.instance.AttackCd);
        canAttack = true;
    }


    void Attack(float _strenght)
    {
        //GetComponent<PlayerMovement>().animator.SetTrigger("Attack");
        if (GetComponent<Player>().ActualPlayerState != PlayerState.DEAD)
        {
            #region Range
            float it = -GameManager.instance.SideRangeDeg;
            for (int i = 0; i < GameManager.instance.SideRangeDeg * 2; i++)//do all the raycast
            {
                RaycastHit hit;
                #region Raycast Calcul
                //next 5 lines calcul the right angle and do the raycast
                middleDirAngle = Mathf.Atan2(transform.TransformDirection(Vector3.forward).z, transform.TransformDirection(Vector3.forward).x);//si ça marche plus faut faire le transform.TransformDirection après les calcules
                float angle = middleDirAngle - Mathf.Deg2Rad * it;
                Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                Debug.DrawRay((new Vector3(dir.x / 2.5f,0,dir.z / 2.5f) + transform.localPosition), dir * GameManager.instance.Range, Color.blue, 5.0f);
                Physics.Raycast((new Vector3(dir.x / 3, 0, dir.z / 3) + transform.localPosition), dir, out hit, GameManager.instance.Range,layerMask);
                #endregion

                #region HitCondition
                if (hit.transform != null && hit.transform.tag == "Player")//if we hit a player we push him
                {
                    //Vector3 hitDir = new Vector3(hit.transform.position.x - transform.position.x, 0, hit.transform.position.z - transform.position.z);
                    hit.rigidbody.AddForce(new Vector3(dir.x, 1, dir.z) * _strenght, ForceMode.Impulse);
                    //Debug.Log(hit.transform.name + " has been hit");
                    hit.transform.GetComponent<PlayerAttack>().HitTag(gameObject);
                    hit.transform.GetComponent<Player>().ActualPlayerState = PlayerState.FLYING;
                    return;
                }
                if (hit.transform != null && hit.transform.tag == "FishBag")//if we hit a FishBag we do things
                {
                    hit.transform.GetComponent<FishBag>().Damage(gameObject);
                    return;
                }
                if (hit.transform != null && hit.transform.tag == "Shield")//if we hit the shield, he loose HP
                {
                    CenterManager.instance.DealDamage();
                    return;
                }

                #endregion

                #region Don't Do To Much
                if (it < GameManager.instance.SideRangeDeg)
                    it += GameManager.instance.SideRangeDeg / GameManager.instance.SideRangeDeg * 4;
                else
                    break;
                int xcount = Random.Range(0, 5);

                switch (xcount)
                {
                    case 0:
                        FindObjectOfType<AudioManager>().Play("Hurt1");
                        break;
                    case 1:
                        FindObjectOfType<AudioManager>().Play("Hurt2");
                        break;
                    case 2:
                        FindObjectOfType<AudioManager>().Play("Hurt3");
                        break;
                    case 3:
                        FindObjectOfType<AudioManager>().Play("Hurt4");
                        break;
                    case 4:
                        FindObjectOfType<AudioManager>().Play("Hurt5");
                        break;
                    case 5:
                        FindObjectOfType<AudioManager>().Play("Hurt6");
                        break;
                }
                #endregion

            }
            #endregion
        }
    }
    #endregion

    public void AddSpeBarrePoint(int _point)
    {
        currentSpecial += _point;
        speBarreSlider.value = currentSpecial;
    }

    public void HitTag(GameObject _player)
    {
        StartCoroutine(WhoHitMe(_player, 5));
    }

    IEnumerator WhoHitMe(GameObject _player, float _s)
    {
        playerHitedBy = _player;
        yield return new WaitForSecondsRealtime(_s);
        playerHitedBy = null;
    }
}
