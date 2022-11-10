using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    public SkinnedMeshRenderer skinedRenderer;

    void Start()
    {
        Player player = GetComponent<Player>();
        Material[] allPlayerMaterial = Resources.LoadAll<Material>("Material_Pingouin");
        skinedRenderer.material = allPlayerMaterial[player.playerID];
    }
}