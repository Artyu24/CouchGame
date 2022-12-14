using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region UI
    public GameObject[] speBarrePrefabs = new GameObject[4];
    [SerializeField]
    private GameObject interfaceUIPrefab;
    [SerializeField]
    private Sprite[] interfaceUIPrefabPP = new Sprite[4];
    public GameObject InterfaceUiPrefab => interfaceUIPrefab;
    
    private GameObject[] playersInterface = new GameObject[4];
    public GameObject[] PlayersInterface => playersInterface;

    public GameObject canvasUI, scoreboardUI, pausePanel, optionsPanel;
    //public List<GameObject> speBarreParentList = new List<GameObject>();
    #endregion

    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    public List<Gamepad> manettes = new List<Gamepad>();
    [HideInInspector]
    public List<Player> playersSortedByScore = new List<Player>();


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


        //Debug.Log("Player count : " + players.Count);
    }
    public void FindCanvas()
    {
        canvasUI = GameObject.FindGameObjectWithTag("Canvas");
        FindPrefabs();
    }

    public void AddPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("LostPlayer");

        int id = players.Count;

        Player dataPlayer = player.GetComponent<Player>();
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;
        
        //Dict Players
        players.Add(id, dataPlayer);
        
        Init(id, player);
        
        player.tag = "Player";
        if (SceneManager.GetActiveScene().name != "LobbyV1_Working")
        {
            ScoreManager.instance.UpdateScores();
        }

        CameraManager.Instance.AddPlayerTarget(player.transform, dataPlayer.playerID + 1);

        if (players.Count >= 2 && GameManager.instance.ActualGameState == GameState.WAIT)
        {
            GameManager.instance.ActualGameState = GameState.INIT;
            StartCoroutine(GameManager.instance.TimerSound());
        }
    }

    private void FindPrefabs()
    {
        //Find all prefab
        speBarrePrefabs = Resources.LoadAll<GameObject>("UI/SpeBarreCharge");
        interfaceUIPrefab = Resources.Load<GameObject>("UI/PlayersUI/JUI");
        scoreboardUI = canvasUI.transform.GetChild(0).GetChild(1).gameObject;
        pausePanel = canvasUI.transform.GetChild(3).gameObject; 
        optionsPanel = canvasUI.transform.GetChild(4).gameObject; 
    }


    public void Init(int i, GameObject player)
    {
        if (scoreboardUI != null)
        {
            //Vider les score de manche pour les joueurs
            players[i].score = 0;

            //changer le nom dans la hierarchie
            players[i].name = "Player " + (i+1);

            //Spawn at point
            Transform posSpawn = PointAreaManager.instance.PlayerSpawnStart[i];
            if(posSpawn)
                player.transform.position = posSpawn.position;
            else
                player.transform.position = PointAreaManager.instance.GetPlayerRandomPos().position;

            if (SceneManager.GetActiveScene().name != "LobbyV1_Working")
            {
                //Parent UI par Player
                GameObject playerInterfaceTempo = Instantiate(interfaceUIPrefab, scoreboardUI.transform);
                playerInterfaceTempo.name = "JUI " + (i + 1);
                playersInterface[i] = playerInterfaceTempo;
                playersInterface[i].GetComponentInChildren<Image>().sprite = interfaceUIPrefabPP[i];
                playersInterface[i].GetComponent<PlayerUIInfo>().PlayerID = (i);

                //Text du score par Player
                ScoreManager.instance.InstantiateScoreText(i);

                //Sp? barre par player
                GameObject speBarreTemp = Instantiate(speBarrePrefabs[i], playerInterfaceTempo.transform.GetChild(1).transform);
                speBarreTemp.name = "Sp?ChargeBarre " + (1);
                player.GetComponent<PlayerAttack>().SpeBarreSlider = speBarreTemp.GetComponent<Slider>();
                player.GetComponent<PlayerAttack>().SpeBarreSlider.value = 0;

                //arrowPlayer
                player.GetComponent<ArrowPlayer>().fleche.SetActive(true);
                player.GetComponent<ArrowPlayer>().texte.SetActive(true);
            }
            
        }
        else
        {
            Debug.Log("Il manque l'UI du scoreboard ou il n'a pas le tag Scoreboard");
        }
    }
}
