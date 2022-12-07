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
        if (sensInverse == true)
        {
          timeCounter += Time.deltaTime * speed;              
        }
        float x = Mathf.Cos(timeCounter) * position;
        float z = Mathf.Sin(timeCounter) * position;

        transform.position = new Vector3(x, gameObject.transform.position.y, z);

        if (playerAttack.bumperIsCharged == true)
        {
            //attackCharged = true;
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

        if(dotResult > 0)
        {
            if (playerAttack.BumperIsCharged == true)
            {
                speed = speedHitBySuperStreghtInverse;
                StartCoroutine(ForSensNoramal());

            }
            else
            {
                speed = speedInverseAiguilleMontre;
                StartCoroutine(ForSensNoramal());   
            }

        }
        else
        {
            if(playerAttack.BumperIsCharged == true)
            {
                speed = speedHitBySuperStrenght;
                StartCoroutine(ForSensInverse());

            }
            else
            {
                speed = speedAiguilleMontre;
                StartCoroutine(ForSensNoramal());
            }
        }
    }

    public IEnumerator ForSensNoramal()
    {
        sensNormal = true;
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        playerAttack.BumperIsCharged = false;
        sensNormal = false;
    }
    public IEnumerator ForSensInverse()
    {
        sensInverse = true;
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        playerAttack.BumperIsCharged = false;
        sensInverse = false;
    }
}
