using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerAttack : MonoBehaviour
{
    #region Variable
    [Header("Variable")]
    [SerializeField] private bool canAttack = true;

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
            StartCoroutine(AttackCoroutine(strenght));
    }
    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        float strenght = GameManager.instance.SpecialStrenght;
        if (ctx.started && canAttack)
            StartCoroutine(AttackCoroutine(strenght));
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
        GetComponent<PlayerMovement>().animator.SetTrigger("Attack");
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
                Debug.DrawRay(transform.position, dir * GameManager.instance.Range, Color.blue, 1.0f);
                Physics.Raycast(transform.position, dir, out hit, GameManager.instance.Range,layerMask);
                #endregion

                #region HitCondition
                if (hit.transform != null && hit.transform.tag == "Player")//if we hit a player we push him
                {
                    //Vector3 hitDir = new Vector3(hit.transform.position.x - transform.position.x, 0, hit.transform.position.z - transform.position.z);
                    hit.rigidbody.AddForce(dir * _strenght, ForceMode.Impulse);
                    Debug.Log(hit.transform.name + " has been hit");
                }
                else if (hit.transform != null && hit.transform.tag == "PointArea")//if we hit a point Area we do things
                {
                    hit.transform.GetComponent<PointArea>().Damage(this.gameObject);
                }
                #endregion

                #region Don't Do To Much
                if (it < GameManager.instance.SideRangeDeg)
                    it += GameManager.instance.SideRangeDeg / GameManager.instance.SideRangeDeg * 4;
                else
                    break;
                #endregion

            }
            #endregion
        }
    }
    #endregion
}
