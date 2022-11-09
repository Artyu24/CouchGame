using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    void Start()
    {
        Player player = GetComponent<Player>();
        Material[] allPlayerMaterial = Resources.LoadAll<Material>("Material_Pingouin");
        GetComponentInChildren<MeshRenderer>().material = allPlayerMaterial[player.playerID];
    }
}