using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject playerInMiddle;
    public GameObject PlayerInMiddle { get => playerInMiddle; set => playerInMiddle = value; }

    [Header("Variables Game Feel")]
    [Tooltip("Vitesse de rotation des anneaux")]
    [SerializeField] private float circleRotationSpeed = 5;
    public float CircleRotationSpeed => circleRotationSpeed;
    [Tooltip("Vitesse de d�placement des joueurs")]
    [SerializeField] private float movementSpeed;
    public float MovementSpeed => movementSpeed;
    [Tooltip("Zone morte des joysticks de la manette")]
    [SerializeField] private float deadZoneController = 0.3f;
    public float DeadZoneController => deadZoneController;

    #region Attack
    [Header("Attack Variable")]
    private float range = 0.1f;
    public float Range {get => range; private set => range = value;}

    private float sideRangeDeg = 20.0f;
    public float SideRangeDeg { get => sideRangeDeg; private set => sideRangeDeg = value; }

    private float normalStrenght;
    public float NormalStrenght { get => normalStrenght; private set => normalStrenght = value; }

    private float specialStrenght;
    public float SpecialStrenght { get => specialStrenght; private set => specialStrenght = value; }

    private float attackCd = 1.5f;
    public float AttackCd { get => attackCd; private set => attackCd = value; }
    #endregion

    private GameState actualGameState = GameState.MENU;
    public GameState ActualGameState { get => actualGameState; set => actualGameState = value; }


    [Tooltip("Liste des anneaux du terrain")]
    [SerializeField] private GameObject[] tabCicle;
    [Tooltip("Vitesse des points de spawn")]
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
