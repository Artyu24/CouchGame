using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class RailedBumper : MonoBehaviour, IInteractable
{
    float timeCounter = 0;

    float speedAiguilleMontre;
    float speedInverseAiguilleMontre;
    float speedHitBySuperStrenght;
    float speedHitBySuperStreghtInverse;
    float position;
    float speed;
    Vector3 igloo;
    PlayerAttack playerAttack;
    bool attackCharged = false;

    public float strenghtPlayerAttack;


    bool sensNormal = false;
    bool sensInverse = false;


    public GameObject playerTriggeredBy;

    private void Start()
    {
        igloo = new Vector3(0, 0, 0);
        speedAiguilleMontre = GameManager.instance.SpeedBumper;
        speedInverseAiguilleMontre = -GameManager.instance.SpeedBumper;
        speedHitBySuperStrenght = -GameManager.instance.SpeedBumperCharged;
        speedHitBySuperStreghtInverse = GameManager.instance.SpeedBumperCharged;
        position = gameObject.transform.position.x;
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        transform.LookAt(igloo);

        if (sensNormal == true)
        {
            timeCounter += Time.deltaTime * speed;
        }
        else
        {
            timeCounter += Time.deltaTime * 0;
        }
        if (sensInverse == true)
        {
            timeCounter += Time.deltaTime * speed;
        }
        else
        {
            timeCounter += Time.deltaTime * 0;

        }
        float x = Mathf.Cos(timeCounter) * position;
        float z = Mathf.Sin(timeCounter) * position;

        transform.position = new Vector3(x, gameObject.transform.position.y, z);

        

    }

    public void Interact(Player player = null)
    {
        attackCharged = strenghtPlayerAttack > GameManager.instance.NormalStrenght;
        playerTriggeredBy = player.gameObject;
        Vector3 playerOrient = player.transform.forward;
        Vector3 bumperOrient = transform.right;
        playerOrient.y = 0;
        bumperOrient.y = 0;
        playerOrient.Normalize();
        bumperOrient.Normalize();
        float dotResult = Vector3.Dot(playerOrient, bumperOrient);

        if (dotResult > 0)
        {
            if (attackCharged == true)
            {
                speed = speedHitBySuperStreghtInverse;
                sensNormal = true;

                StartCoroutine(ForSensNoramalCharged());

            }
            else
            {
                speed = speedAiguilleMontre;
                sensNormal = true;

                StartCoroutine(ForSensNoramal());

            }

        }
        else
        {
            if (attackCharged == true)
            {
                speed = speedHitBySuperStrenght;
                sensInverse = true;

                StartCoroutine(ForSensInverseCharged());

            }
            else
            {
                speed = speedInverseAiguilleMontre;
                sensInverse = true;

                StartCoroutine(ForSensInverse());

            }
        }
    }

    public IEnumerator ForSensNoramal()
    {
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        sensNormal = false;
        attackCharged = false;
    }
    public IEnumerator ForSensNoramalCharged()
    {
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistanceCharged);
        sensNormal = false;
        attackCharged = false;
    }
    public IEnumerator ForSensInverse()
    {
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        sensInverse = false;
        attackCharged = false;
    }
    public IEnumerator ForSensInverseCharged()
    {
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistanceCharged);
        sensInverse = false;
        attackCharged = false;
    }
}
