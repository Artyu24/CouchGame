using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerAttack : MonoBehaviour
{
    [Header("Variable")]
    [SerializeField] private float normalStrenght;
    [SerializeField] private float specialStrenght;

    [Header("Range")]
    [SerializeField] private float range = 0.1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField][Tooltip("La partie sur le coté en Degré (prendre en compte x2 pour l'amplitude total)")] private float sideRangeDeg = 20.0f;
    private float middleDirAngle;


    #region InputSysteme
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        float strenght = normalStrenght;
        if (ctx.started)
            Attack(strenght);
    }
    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        float strenght = specialStrenght;
        if (ctx.started)
            Attack(strenght);
    }
    #endregion

    void Attack(float _strenght)
    {
        GetComponent<PlayerMovement>().animator.SetTrigger("Attack");
        if (GetComponent<Player>().ActualPlayerState != PlayerState.DEAD)
        {
            #region Range
            float it = -sideRangeDeg;
            for (int i = 0; i < sideRangeDeg * 2; i++)//do all the raycast
            {
                RaycastHit hit;
                #region Raycast Calcul
                //next 5 lines calcul the right angle and do the raycast
                middleDirAngle = Mathf.Atan2(transform.TransformDirection(Vector3.forward).z, transform.TransformDirection(Vector3.forward).x);//si ça marche plus faut faire le transform.TransformDirection après les calcules
                float angle = middleDirAngle - Mathf.Deg2Rad * it;
                Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                Debug.DrawRay(transform.position, dir * range, Color.blue, 1.0f);
                Physics.Raycast(transform.position, dir, out hit, range,layerMask);
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
                    hit.transform.GetComponent<PointArea>().Dammage(this.gameObject);
                }
                #endregion

                #region Don't Do To Much
                if (it < sideRangeDeg)
                    it += sideRangeDeg / sideRangeDeg * 4;
                else
                    break;
                #endregion

            }
            #endregion
        

        }
    }
}
