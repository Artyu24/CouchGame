using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class CenterManager : MonoBehaviour
{
    public static CenterManager instance;


    [SerializeField] private int TimeBtwNextCenter;
    private Bridge[] bridges = new Bridge[4];
    private CenterState actualCenterState = CenterState.PROTECTION;
    public CenterState ActualCenterState { get => actualCenterState; set => actualCenterState = value; }
    private int healthPoint;
    [SerializeField] private int maxHealthPoint = 10;
    [SerializeField] private VisualEffect shieldEffect;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            try
            {
                bridges[i] = transform.GetChild(i).gameObject.GetComponent<Bridge>();
            }
            catch (Exception)
            {
                Debug.Log("IL MANQUE DES BRIDGE");
                return;
            }
        }

        healthPoint = maxHealthPoint;
        transform.position = Vector3.zero;
        ActivateRandomBridge();
        ActivateShield();
    }

    private void ActivateShield()
    {
        healthPoint = maxHealthPoint;
        GetComponent<SphereCollider>().enabled = true;
        //Activer le bo shield
        shieldEffect.Play();
        StartCoroutine(StopShieldAnimation());
    }

    private IEnumerator StopShieldAnimation()
    {
        yield return new WaitForSeconds(3f);
        shieldEffect.pause = true;
    }

    private void DesactivateShield()
    {
        GetComponent<SphereCollider>().enabled = false;
        actualCenterState = CenterState.ACCESS;
        //D�truire le bo shield
        shieldEffect.pause = false;
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
        StartCoroutine(ResetBridge());
    }

    public void DealDamage()
    {
        healthPoint--;
        if(healthPoint <= 0)
            DesactivateShield();
    }

    private IEnumerator ResetBridge()
    {
        yield return new WaitForSeconds(TimeBtwNextCenter);
        ActivateRandomBridge();
    }
}