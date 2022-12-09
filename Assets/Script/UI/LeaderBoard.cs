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
            //reset l'anim, voir mettre une anim de danse
            switch (i)
            {
                case 0:
                    //Jouer anim du premier
                    break; 
                case 1:
                    //Jouer anim du second
                    break; 
                case 2:
                    //Jouer anim du troisième
                    break;
                case 3:
                    //Jouer anim du dernier + KaKawai
                    break;
                default:
                    break;
            }
        }

    }
}
