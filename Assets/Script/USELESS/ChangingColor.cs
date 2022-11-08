using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColor : MonoBehaviour
{
    private Color activeColor;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();

        switch (player.playerID)
        {
            case 0:
                activeColor = Color.blue;
                break;
            case 1:
                activeColor = Color.red;
                break;
            case 2:
                activeColor = Color.yellow;
                break;
            case 3:
                activeColor = Color.green;
                break;
            default:
                break;
        }
        GetComponentInChildren<MeshRenderer>().material.color = activeColor;
    }
}