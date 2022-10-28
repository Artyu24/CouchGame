using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterManager : MonoBehaviour
{
    public static CenterManager instance;

    [SerializeField] private Bridge[] bridges = new Bridge[4];
    private CenterState actualCenterState = CenterState.PROTECTION;
    public CenterState ActualCenterState { get => actualCenterState; set => actualCenterState = value; }
    private int healthPoint;
    private int maxHealthPoint = 10;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        ActivateRandomBridge();
        ActivateShield();
    }

    private void ActivateShield()
    {
        healthPoint = maxHealthPoint;
        GetComponent<SphereCollider>().enabled = true;
        //Activer le bo shield
    }

    private void DesactivateShield()
    {
        GetComponent<SphereCollider>().enabled = false;
        actualCenterState = CenterState.ACCESS;
        //Détruire le bo shield
    }

    private void ActivateRandomBridge()
    {
        int i = Random.Range(0, bridges.Length);
        bridges[i].ActivateBridge();
        actualCenterState = CenterState.PROTECTION;
    }

    public void ActivateAllBridge()
    {
        foreach (Bridge bridge in bridges)
        {
            if(bridge != null)
                bridge.ActivateBridge();
        }
        actualCenterState = CenterState.USE;
    }

    public void DesactivateAllBridge()
    {
        foreach (Bridge bridge in bridges)
        {
            if (bridge != null)
                bridge.DesactivateBridge();
        }

        actualCenterState = CenterState.REGENERATION;
        ActivateShield();
    }

    public void DealDamage()
    {
        healthPoint--;
        if(healthPoint <= 0)
            DesactivateShield();
    }
}
