using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region GameManager
    [Header("Variables du Game Manager")]

    [Tooltip("Temps pour 1 manche en seconde (donc pour une game de 2min30 => 150 sec)")]
    [SerializeField] private float timer;
    [Tooltip("Zone morte des joysticks de la manette")]
    [SerializeField] private float deadZoneController = 0.3f;
    public static GameManager instance;
    private GameState actualGameState = GameState.MENU;
    public GameState ActualGameState { get => actualGameState; set => actualGameState = value; }
    public float Timer { get => timer; set => timer = value; }
    public float DeadZoneController => deadZoneController;
    #endregion
    #region Attack
    [Header("Variable de l'Attack")]

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

    #region Player
    [Header("Variables des Players")]

    private Dictionary<int, Player> players = new Dictionary<int, Player>();
    [SerializeField] private Transform[] spawnList = new Transform[] { };
    [Tooltip("Vitesse de d�placement des joueurs")]
    [SerializeField] private float movementSpeed;
    [Tooltip("Temps du respawn des players en seconde")]
    [SerializeField] private float respawnDelay = 2;
    [Tooltip("Liste des points de spawn")]
    public Transform[] SpawnList => spawnList;
    public float MovementSpeed => movementSpeed;
    public float RespawnDelay => respawnDelay;

    #endregion
    #region Circles
    [Header("Variables des Anneaux")]

    [Tooltip("Liste des anneaux du terrain")]
    [SerializeField] private GameObject[] tabCircle;
    [Tooltip("Vitesse de rotation des anneaux")]
    [SerializeField] private float circleRotationSpeed = 5;
    [Tooltip("...")]
    [SerializeField] private Color colorCircleChoose;
    private List<Color> tabMaterialColor = new List<Color>();
    public GameObject[] TabCircle => tabCircle;
    public float CircleRotationSpeed => circleRotationSpeed;
    public Color ColorCircleChoose => colorCircleChoose;
    public List<Color> TabMaterialColor => tabMaterialColor;
    #endregion
    #region EjectPlates
    [Header("Variables des EjectPlates")]

    [Tooltip("Liste des plaques d'ejection du player au centre")]
    [SerializeField] private GameObject[] ejectPlates;
    [Tooltip("Nombre de plaques à activer pour eject le joueur au centre")]
    [SerializeField] private int numberOfPlate = 3;
    [Tooltip("Couleur que prend la zone quand elle est activée")]
    [SerializeField] private Color activatedColor;
    [Tooltip("Couleur que prend la zone quand elle est spawn")]
    [SerializeField]  private Color activeColor;
    [HideInInspector] public int ejectPlatesActive = 0;
    public GameObject[] EjectPlates => ejectPlates;
    public int NumberOfPlate => numberOfPlate;
    public Color ActivatedColor => activatedColor;
    public Color ActiveColor => activeColor;


    #endregion
    #region Middle
    [Header("Variable de l'Igloo")]

    [Tooltip("Main Camera du jeu")]
    [SerializeField] private Camera cameraScene;
    [Tooltip("Variable pour augmenter ou diminuer le shake de la cam")]
    public float shakePower = 0.05f;
    [Tooltip("Variable pour augmenter ou diminuer le temps du shake de la cam")]
    public float shakeDuration = 0.5f;
    private GameObject playerInMiddle;

    public Camera CameraScene { get => cameraScene; set => cameraScene = value; }
    public float ShakePower => shakePower;
    public float ShakeDuration => shakeDuration;
    public GameObject PlayerInMiddle { get => playerInMiddle; set => playerInMiddle = value; }

    #endregion

    void Awake()
    {   
        if (instance == null)
            instance = this;

        foreach (GameObject circle in TabCircle)
        {
            tabMaterialColor.Add(circle.GetComponent<MeshRenderer>().material.color);
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

    public Transform RandomSpawn()
    {
        int random = Random.Range(0, spawnList.Length);
        return spawnList[random];
    }


}
