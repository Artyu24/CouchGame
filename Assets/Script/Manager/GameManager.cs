using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

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

    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    [Header("Variables des Players")]
    [SerializeField] private Transform[] spawnList = new Transform[] { };
    [Tooltip("Vitesse max de d�placement des joueurs")]
    [SerializeField] private float maxMovementSpeed;
    [Tooltip("Vitesse de d�placement des joueurs dans la slowZone")]
    [SerializeField] private float movSpeedSlowZone;
    [Tooltip("Temps du respawn des players en seconde")]
    [SerializeField] private float respawnDelay = 2;
    [Tooltip("Temps invincibilité des players apres le respawn en seconde")]
    [SerializeField] private int invincibleDelay = 2;
    [Tooltip("Temps de slow des players apres zone slow en seconde")]
    [SerializeField] private float slowDuration = 2;
    //private Dictionary<Player, int> playersScoreGenerals = new Dictionary<Player, int>();

    public Transform[] SpawnList => spawnList;
    public float MovSpeedSlowZone => movSpeedSlowZone;
    public float MaxMovementSpeed => maxMovementSpeed;
    public float RespawnDelay => respawnDelay;

    public int InvincibleDelay => invincibleDelay;


    public float SlowDuration => slowDuration;

    //public Dictionary<Player, int> PlayersScoreGenerals { get => playersScoreGenerals; set => playersScoreGenerals = value; }


    #endregion
    #region Circles
    [Header("Variables des Anneaux")]

    [Tooltip("Vitesse de rotation des anneaux")]
    [SerializeField] private float circleRotationSpeed = 5;
    [Tooltip("PAS TOUCHE"), SerializeReference]
    private List<GameObject> tabCircle;
    public List<GameObject> TabCircle => tabCircle;
    [Tooltip("PAS TOUCHE"), SerializeReference]
    private List<GameObject> circleBlockList;
    public List<GameObject> CircleBlockList => circleBlockList;

    [Tooltip("...")]
    [SerializeField] private Color colorCircleChoose;
    private List<Color> tabMaterialColor = new List<Color>();
    public float CircleRotationSpeed => circleRotationSpeed;
    public Color ColorCircleChoose => colorCircleChoose;
    public List<Color> TabMaterialColor => tabMaterialColor;
    #endregion
    #region EjectPlates
    [Header("Variables des EjectPlates")]

    [Tooltip("Liste des plaques d'ejection du player au centre")]
    private List<GameObject> ejectPlates = new List<GameObject>();
    [Tooltip("Nombre de plaques à activer pour eject le joueur au centre")]
    [SerializeField] private int numberOfPlate = 3;
    [Tooltip("Couleur que prend la zone quand elle est activée")]
    [SerializeField] private Color activatedColor;
    [Tooltip("Couleur que prend la zone quand elle est spawn")]
    [SerializeField]  private Color activeColor;
    [HideInInspector] public int ejectPlatesActive = 0;
    public List<GameObject> EjectPlates => ejectPlates;
    public int NumberOfPlate => numberOfPlate;
    public Color ActivatedColor => activatedColor;
    public Color ActiveColor => activeColor;

    public List<Gamepad> manettes = new List<Gamepad>();

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
    #region UI
    public GameObject speBarrePrefab;
    public List<GameObject> speBarreParentList = new List<GameObject>();
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        /*else
            Destroy(this.gameObject);*/
        

        foreach (GameObject circle in TabCircle)
        {
            tabMaterialColor.Add(circle.GetComponent<MeshRenderer>().material.color);
        }

    }

    public void AddPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("LostPlayer");
        manettes.Add(Gamepad.current);
        //Debug.Log("manette  : " + Gamepad.current);
        Player dataPlayer = player.GetComponent<Player>();
        players.Add(players.Count + 1, dataPlayer);
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;

        switch (dataPlayer.playerID)
        {
            case 1:
                player.transform.position = spawnList[0].position;
                GameObject temp1 = Instantiate(speBarrePrefab, speBarreParentList[0].transform);
                player.GetComponent<PlayerAttack>().speBarre = temp1;
                temp1.name = "SpéChargeBarre " + (1);
                break;
            case 2:
                player.transform.position = spawnList[1].position;
                GameObject temp2 = Instantiate(speBarrePrefab, speBarreParentList[1].transform);
                player.GetComponent<PlayerAttack>().speBarre = temp2;
                temp2.name = "SpéChargeBarre " + (2);
                break;
            case 3:
                player.transform.position = spawnList[2].position;
                GameObject temp3 = Instantiate(speBarrePrefab, speBarreParentList[2].transform);
                player.GetComponent<PlayerAttack>().speBarre = temp3;
                temp3.name = "SpéChargeBarre " + (3);
                break;
            case 4:
                player.transform.position = spawnList[3].position;
                GameObject temp4 = Instantiate(speBarrePrefab, speBarreParentList[3].transform);
                player.GetComponent<PlayerAttack>().speBarre = temp4;
                temp4.name = "SpéChargeBarre " + (4);
                break;
            default:
                break;
        }
        player.tag = "Player";
        ScoreManager.instance.UpdateScores();
    }

    public Transform RandomSpawn()
    {
        int random = Random.Range(0, spawnList.Length);
        return spawnList[random];
    }
}
