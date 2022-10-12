using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerInMiddle;

    [Header("Setup Data")]
    [SerializeField] private float circleRotationSpeed = 5;
    public float CircleRotationSpeed => circleRotationSpeed;
    [SerializeField] private float movementSpeed;
    public float MovementSpeed => movementSpeed;
    [SerializeField] private float deadZoneController = 0.3f;
    public float DeadZoneController => deadZoneController;

    private GameState actualGameState = GameState.MENU;
    public GameState ActualGameState { get => actualGameState; set => actualGameState = value; }


    [SerializeField] private GameObject[] tabCicle;
    [SerializeField] private Transform[] spawnList = new Transform[] { };
    public GameObject[] TabCicle => tabCicle;
    public Transform[] SpawnList => spawnList;

    private Dictionary<int, Player> players = new Dictionary<int, Player>();

    void Awake()
    {   
        if (instance == null)
            instance = this;
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

    public Transform RandomSpawn()
    {
        int random = Random.Range(0, spawnList.Length);
        return spawnList[random];
    }
}
