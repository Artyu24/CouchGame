using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerInMiddle;

    private GameState actualGameState = GameState.MENU;

    public GameState ActualGameState { get => actualGameState; set => actualGameState = value; }

    public Transform[] spawnList = new Transform[] { };

    [SerializeField] private GameObject[] tabCicle;
    public GameObject[] TabCicle => tabCicle;
    
    private Dictionary<int, Player> players = new Dictionary<int, Player>();

    void Awake()
    {   
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            AddPlayer();
        }
    }

    public void AddPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("LostPlayer");
        Player dataPlayer = player.GetComponent<Player>();
        players.Add(players.Count + 1, dataPlayer);
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;

        switch (dataPlayer.playerID)
        {
            case 1:
                player.transform.position = spawnList[0].position;
                break;
            case 2:
                player.transform.position = spawnList[1].position;
                break;
            case 3:
                player.transform.position = spawnList[2].position;
                break;
            case 4:
                player.transform.position = spawnList[3].position; 
                break;
            default:
                break;
        }
        player.tag = "Player";
    }
}
