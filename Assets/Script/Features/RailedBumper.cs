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


    bool sensNormal = false;
    bool sensInverse = false;
    private void Start()
    {
        igloo = new Vector3(0, 0, 0);
        speedAiguilleMontre = GameManager.instance.SpeedBumper;
        speedInverseAiguilleMontre = -GameManager.instance.SpeedBumper;
        speedHitBySuperStrenght = 2;
        speedHitBySuperStreghtInverse = -2;
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

        if (playerAttack.bumperIsCharged == true)
        {
            attackCharged = true;
        }

    }

    public void Interact(Player player = null)
    {
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

                StartCoroutine(ForSensNoramal());
                Debug.Log("1");

            }
            else
            {
                speed = speedAiguilleMontre;
                sensNormal = true;

                StartCoroutine(ForSensNoramal());
                Debug.Log("2");

            }

        }
        else
        {
            if (attackCharged == true)
            {
                speed = speedHitBySuperStrenght;
                sensInverse = true;

                StartCoroutine(ForSensInverse());
                Debug.Log("3");

            }
            else
            {
                speed = speedInverseAiguilleMontre;
                sensInverse = true;

                StartCoroutine(ForSensInverse());
                Debug.Log("4");

            }
        }
    }

    public IEnumerator ForSensNoramal()
    {
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        sensNormal = false;
        attackCharged = false;
        Debug.Log("Sens normal coroutine false");

    }
    public IEnumerator ForSensInverse()
    {
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        sensInverse = false;
        attackCharged = false;
        Debug.Log("Sens inverse coroutine false");

    }
}
