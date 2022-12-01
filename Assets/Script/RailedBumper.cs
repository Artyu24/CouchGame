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
    public Transform igloo;

    bool sensNormal = false;
    bool sensInverse = false;
    private void Start()
    {
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
        if(gameObject.transform.position.x < 0)
        {
            if (player.transform.position.z > gameObject.transform.position.z)
            {
                StartCoroutine(ForSensNoramal());
                Debug.Log("Sensnormal");
            }
            if (player.transform.position.z < gameObject.transform.position.z)
            {
                StartCoroutine(ForSensInverse());
                Debug.Log("SensInverse");
            }
        }
        if(gameObject.transform.position.x > 0)
        {
            if (player.transform.position.z < gameObject.transform.position.z)
            {
                StartCoroutine(ForSensNoramal());
                Debug.Log("Sensnormal");
            }
            if (player.transform.position.z > gameObject.transform.position.z)
            {
                StartCoroutine(ForSensInverse());
                Debug.Log("SensInverse");
            }
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
