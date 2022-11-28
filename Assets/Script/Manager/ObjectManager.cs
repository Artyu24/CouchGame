using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    [Header("BOMB")]
    [Tooltip("la taille de l'explosion")] public int blastRadius = 4;
    [Tooltip("Temps avant l'xplosion")] public int timeBeforeExplosion = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
