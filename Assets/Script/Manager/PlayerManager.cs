using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region UI
    public GameObject speBarrePrefab;
    [SerializeField]
    private GameObject interfaceUIPrefab;
    public GameObject InterfaceUiPrefab => interfaceUIPrefab;
    
    private GameObject[] playersInterface = new GameObject[4];
    public GameObject[] PlayersInterface => playersInterface;

    public GameObject canvasUI;
    public GameObject scoreboardUI;
    //public List<GameObject> speBarreParentList = new List<GameObject>();
    #endregion

    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    public List<Gamepad> manettes = new List<Gamepad>();


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        //Find all prefab
        speBarrePrefab = Resources.Load<GameObject>("UI/SpeBarreCharge");
        interfaceUIPrefab = Resources.Load<GameObject>("UI/PlayersUI/JUI");
        scoreboardUI = GameObject.FindGameObjectWithTag("Scoreboard");

        //Debug.Log("Player count : " + players.Count);
    }
    public void FindCanvas()
    {
        canvasUI = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void AddPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("LostPlayer");
        
        //manettes.Add(Gamepad.current); //pbm il prend pas la bonne manette

        /*for (int i = 0; i < manettes.Count; i++)
        {
            Debug.Log(i + " : " + manettes[i]);
        }*/

        int xcount = Random.Range(0, 3);

        /*switch (xcount)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("Spawn/respawn1");
                break;
            case 1:
                FindObjectOfType<AudioManager>().Play("Spawn/respawn2");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play("Spawn/respawn3");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Play("Spawn/respawn4");
                break;
        }*/

        int id = players.Count;

        Player dataPlayer = player.GetComponent<Player>();
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;
        
        //Dict Players
        players.Add(id, dataPlayer);
        
        Init(id, player);
        
        player.tag = "Player";
        ScoreManager.instance.UpdateScores();
    }


    public void Init(int i, GameObject player)
    {
        if (scoreboardUI != null)
        {
            //Vider les score de manche pour les joueurs
            players[i].score = 0;

            //Spawn at point
            player.transform.position = PointAreaManager.instance.PlayerSpawnStart[i].position;

            //Parent UI par Player
            GameObject playerInterfaceTempo = Instantiate(interfaceUIPrefab, scoreboardUI.transform);
            playersInterface[i] = playerInterfaceTempo;

            //Text du score par Player
            ScoreManager.instance.InstantiateScoreText(i);

            //Spé barre par player
            GameObject speBarreTemp = Instantiate(speBarrePrefab, playerInterfaceTempo.transform.GetChild(1).transform);
            speBarreTemp.name = "SpéChargeBarre " + (1);
            player.GetComponent<PlayerAttack>().SpeBarreSlider = speBarreTemp.GetComponent<Slider>();
        }
        else
        {
            Debug.Log("Il manque l'UI du scoreboard ou il n'a pas le tag Scoreboard");
        }
    }
}
