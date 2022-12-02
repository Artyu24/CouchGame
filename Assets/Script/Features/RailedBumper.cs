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
    float position;
    Vector3 igloo;

    bool sensNormal = false;
    bool sensInverse = false;
    private void Start()
    {
        igloo = new Vector3(0, 0, 0);
        speedAiguilleMontre = GameManager.instance.SpeedBumper;
        speedInverseAiguilleMontre = -GameManager.instance.SpeedBumper;
        speedHitBySuperStrenght = 2;
        position = gameObject.transform.position.x;
    }

    private void Update()
    {
        transform.LookAt(igloo);
        if (sensNormal == true)
        {
            timeCounter += Time.deltaTime * speedAiguilleMontre;

            float x = Mathf.Cos(timeCounter) * position;
            float z = Mathf.Sin(timeCounter) * position;

            transform.position = new Vector3(x, gameObject.transform.position.y, z);

        }
        if(sensInverse == true)
        {
            timeCounter += Time.deltaTime * speedInverseAiguilleMontre;

            float x = Mathf.Cos(timeCounter) * position;
            float z = Mathf.Sin(timeCounter) * position;

            transform.position = new Vector3(x, gameObject.transform.position.y, z);

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
            StartCoroutine(ForSensNoramal());

        }
        else
        {
            StartCoroutine(ForSensInverse());
        }
    }

    public IEnumerator ForSensNoramal()
    {
        sensNormal = true;
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        sensNormal = false;
    }
    public IEnumerator ForSensInverse()
    {
        sensInverse = true;
        yield return new WaitForSeconds(GameManager.instance.BumperMovementDistance);
        sensInverse = false;
    }
}
