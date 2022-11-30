using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class RailedBumper : MonoBehaviour
{
    float timeCounter = 0;

    float speedAiguilleMontre;
    float speedInverseAiguilleMontre;
    float speedHitBySuperStrenght;
    float position;
    public Transform igloo;
    private void Start()
    {
        speedAiguilleMontre = 2;
        speedInverseAiguilleMontre = -2;
        speedHitBySuperStrenght = 2;
        position = 6;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime* speedAiguilleMontre;

        float x = Mathf.Cos(timeCounter)* position;
        float z = Mathf.Sin(timeCounter)* position;

        transform.position = new Vector3(x, gameObject.transform.position.y, z);
        transform.LookAt(igloo);
    }
}
