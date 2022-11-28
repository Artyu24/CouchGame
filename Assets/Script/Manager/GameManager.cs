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
    public static GameManager instance;
    
    #region GameManager

    [Header("Variables du Game Manager")]

    [Tooltip("Temps pour 1 manche en seconde (donc pour une game de 2min30 => 150 sec)")]
    [SerializeField] private float timer;
    [Tooltip("Zone morte des joysticks de la manette")]
    [SerializeField] private float deadZoneController = 0.3f;
    [SerializeField] private int nbrFishBag = 2;
    public int NbrFishBag => nbrFishBag;
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

    [SerializeField] private float interactionCD = 1.5f;
    public float InteractionCD { get => interactionCD; private set => interactionCD = value; }

    #endregion
    #region Player

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
    [Header("Variables des ChocWave")]
    [SerializeField] private float radiusMax = 1.5f;
    public float RadiusMax { get => radiusMax; private set => radiusMax = value; }
    [SerializeField] private float growingSpeed = 1.5f;
    public float GrowingSpeed { get => growingSpeed; private set => growingSpeed = value; }
    [SerializeField] private float pushForce = 1.5f;
    public float PushForce { get => pushForce; private set => pushForce = value; }

    [Header("Variables des Meteorite")]
    [SerializeField] private float radiusMaxExplosion = 1.5f;
    public float RadiusMaxExplosion { get => radiusMaxExplosion; private set => radiusMaxExplosion = value; }
    [SerializeField] private float growingSpeedExplossion = 1.5f;
    public float GrowingSpeedExplosion { get => growingSpeedExplossion; private set => growingSpeedExplossion = value; }
    [SerializeField] private float pushForceExplosion = 1.5f;
    public float PushForceExplosion { get => pushForceExplosion; private set => pushForceExplosion = value; }
    [SerializeField] private float speedMeteorite = 1.5f;
    public float SpeedMeteorite { get => speedMeteorite; private set => speedMeteorite = value; }




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
    public List<GameObject> tabCircle;
    public List<GameObject> circleBlockList;

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
    public GameObject pausePanel;
    public GameObject button;
    //public GameObject optionPanel;

    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        /*else
            Destroy(this.gameObject);*/

        for (int i = 0; i < tabCircle.Count; i++)
        {
            if (tabCircle[i] == null)
                tabCircle.Remove(tabCircle[i]);
        }

        if (tabCircle.Count == 0)
        {
            Debug.Log("NO PLATFORM ON GAME");
            return;
        }

        foreach (GameObject circle in tabCircle)
        {
            tabMaterialColor.Add(circle.GetComponent<MeshRenderer>().material.color);
        }

    }
    private void Start()
    {
        cameraScene = Camera.FindObjectOfType<Camera>();
    }

    public void Update()
    {
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
