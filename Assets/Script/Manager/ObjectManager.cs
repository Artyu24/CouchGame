using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    [Header("General"), SerializeField]
    private int itemPossible;
    [SerializeField]
    private float cdGeneral;

    [Header("Multiplier"), SerializeField] 
    private float cdMultiplier;
    private GameObject multiplierObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public IEnumerator StopMultiplier(Player player)
    {
        yield return new WaitForSeconds(cdMultiplier);
        player.Multiplier = false;
        //Reload
    }
}
