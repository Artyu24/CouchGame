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
        }
    }
}
