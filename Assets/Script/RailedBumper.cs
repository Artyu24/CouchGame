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
        speedAiguilleMontre = 2;
        speedInverseAiguilleMontre = -2;
        speedHitBySuperStrenght = 2;
        position = 6;
    }

    private void Update()
    {
        if(sensNormal == true)
        {
            timeCounter += Time.deltaTime * speedAiguilleMontre;

            float x = Mathf.Cos(timeCounter) * position;
            float z = Mathf.Sin(timeCounter) * position;

            transform.position = new Vector3(x, gameObject.transform.position.y, z);
            transform.LookAt(igloo);

        }
        if(sensInverse == true)
        {
            timeCounter += Time.deltaTime * speedInverseAiguilleMontre;

            float x = Mathf.Cos(timeCounter) * position;
            float z = Mathf.Sin(timeCounter) * position;

            transform.position = new Vector3(x, gameObject.transform.position.y, z);
            transform.LookAt(igloo);
        }
        
    }

    public void Interact(Player player = null)
    {
        if(player.transform.position.y - gameObject.transform.position.y < 0)
        {
            StartCoroutine(ForSensNoramal());
        }
        if (player.transform.position.y - gameObject.transform.position.y > 0)
        {
            StartCoroutine(ForSensInverse());
        }

    }

    public IEnumerator ForSensNoramal()
    {
        sensNormal = true;
        yield return new WaitForSeconds(2f);
        sensNormal = false;
    }
    public IEnumerator ForSensInverse()
    {
        sensInverse = true;
        yield return new WaitForSeconds(2f);
        sensInverse = false;
    }
}
