using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    public static LeaderBoard Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            PlayerManager.instance.playersSortedByScore[i].transform.position = spawnPoints[i].position;
            PlayerManager.instance.playersSortedByScore[i].transform.rotation = spawnPoints[i].rotation;
            PlayerManager.instance.playersSortedByScore[i].transform.localScale *= 3;
            //reset l'anim

        }
        //PlayerManager.instance.playersSortedByScore[0].
       /* switch (i)
        {
            case 0:
                PlayerManager.instance.playersSortedByScore[0]
                break;
            case 1:
                PlayerManager.instance.playersSortedByScore[1]
                break;
            case 2:
                PlayerManager.instance.playersSortedByScore[2]
                break;
            case 3:
                PlayerManager.instance.playersSortedByScore[3]
                break;
            default:
                break;
        }*/
    }
}
