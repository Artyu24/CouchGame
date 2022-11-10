using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer renderer;

    void Start()
    {
        Player player = GetComponent<Player>();
        Material[] allPlayerMaterial = Resources.LoadAll<Material>("Material_Pingouin");
        renderer.material = allPlayerMaterial[player.playerID];
    }
}