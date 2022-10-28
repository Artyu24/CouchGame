using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterManager : MonoBehaviour
{
    [SerializeField] private Bridge[] bridges = new Bridge[4];
    private CenterState actualCenterState = CenterState.PROTECTION;

    private void Start()
    {
        transform.position = Vector3.zero;
        ActivateRandomBridge();
    }

    private void ActivateShield()
    {
        GetComponent<SphereCollider>().enabled = true;
        //Activer le bo shield
    }

    private void DesactivateShield()
    {
        GetComponent<SphereCollider>().enabled = false;
        //Détruire le bo shield
    }

    private void ActivateRandomBridge()
    {
        int i = Random.Range(0, bridges.Length);
        bridges[i].ActivateBridge();
    }

    private void ActivateAllBridge()
    {
        foreach (Bridge bridge in bridges)
        {
            if(bridge != null)
                bridge.ActivateBridge();
        }
    }

    private void DesactivateAllBridge()
    {
        foreach (Bridge bridge in bridges)
        {
            if (bridge != null)
                bridge.DesactivateBridge();
        }
    }
}
