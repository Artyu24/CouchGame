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
    [Tooltip("Temps du respawn des players en seconde")]
    [SerializeField] private float respawnDelay = 2;
    public float RespawnDelay => respawnDelay;
    [Tooltip("Zone morte des joysticks de la manette")]
    [SerializeField] private float deadZoneController = 0.3f;
    [Tooltip("Temps pour 1 manche en seconde (donc pour une game de 2min30 => 150 sec)")]
    [SerializeField] private float timer;
    [Tooltip("Couleur que prend la zone quand elle est activée")]
    [SerializeField] private Color activatedColor;
    [Tooltip("Couleur que prend la zone quand elle est spawn")]
    [SerializeField]  private Color activeColor;
    public Color ActivatedColor => activatedColor;
    public Color ActiveColor => activeColor;

    public float DeadZoneController => deadZoneController;
    [SerializeField] private Material colorMaterial, baseMaterial;
    public Material ColorMaterial => colorMaterial;
    public Material BaseMaterial => baseMaterial;

    #region Attack
    [Header("Attack Variable")]
    [SerializeField] private float range = 0.1f;
    public float Range {get => range; private set => range = value;}

    [SerializeField] private float sideRangeDeg = 20.0f;
    public float SideRangeDeg { get => sideRangeDeg; private set => sideRangeDeg = value; }

    [SerializeField] private float normalStrenght;
    public float NormalStrenght { get => normalStrenght; private set => normalStrenght = value; }

    [SerializeField] private float specialStrenght;
    public float SpecialStrenght { get => specialStrenght; private set => specialStrenght = value; }

    [SerializeField] private float attackCd = 1.5f;
    public float AttackCd { get => attackCd; private set => attackCd = value; }
    #endregion

    private GameState actualGameState = GameState.MENU;
    public GameState ActualGameState { get => actualGameState; set => actualGameState = value; }


    [Tooltip("Liste des anneaux du terrain")]
    [SerializeField] private GameObject[] tabCircle;
    [Tooltip("Liste des points de spawn")]
    [SerializeField] private Transform[] spawnList = new Transform[] { };
    [Tooltip("Liste des plaques d'ejection du player au centre")]
    [SerializeField] private GameObject[] ejectPlates;
    

    [HideInInspector] public int ejectPlatesActive = 0;

    public GameObject[] TabCircle => tabCircle;
    public Transform[] SpawnList => spawnList;
    public GameObject[] EjectPlates => ejectPlates;

    public float Timer { get => timer; set => timer = value; }

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
