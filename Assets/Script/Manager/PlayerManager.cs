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
    //public List<GameObject> speBarreParentList = new List<GameObject>();
    #endregion
    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    public List<Gamepad> manettes = new List<Gamepad>();
    [SerializeField] private Transform[] spawnList = new Transform[] { };
    public Transform[] SpawnList => spawnList;

    private GameObject[] interfaceUIPrefab = new GameObject[4];
    public GameObject[] InterfaceUiPrefab => interfaceUIPrefab;
    
    private GameObject[] playersInterface = new GameObject[4];
    public GameObject[] PlayersInterface => playersInterface;


    public GameObject canvasUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        //Find all prefab
        speBarrePrefab = Resources.Load<GameObject>("UI/SpeChargeBarre");
        interfaceUIPrefab = Resources.LoadAll<GameObject>("UI/PlayersUI");
        canvasUI = GameObject.FindGameObjectWithTag("Canvas");

        //Debug.Log("Player count : " + players.Count);
    }

    public void AddPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("LostPlayer");
        player.tag = "Player";
        
        manettes.Add(Gamepad.current);

        int id = players.Count;

        Player dataPlayer = player.GetComponent<Player>();
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;
        
        //Dict Players
        players.Add(id, dataPlayer);
        
        Init(id, player);
        
        ScoreManager.instance.UpdateScores();
    }

    public Transform RandomSpawn()
    {
        int random = Random.Range(0, spawnList.Length);
        return spawnList[random];
    }

    public void FindCanvas()
    {
        canvasUI = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void Init(int i, GameObject player)
    {
        //player.transform.position = spawnList[i].position;

        //Parent UI par Player
        GameObject playerInterfaceTempo = Instantiate(interfaceUIPrefab[i], canvasUI.transform);
        playersInterface[i] = playerInterfaceTempo;

        //Text du score par Player
        ScoreManager.instance.InstantiateScoreText(i);

        //Spé barre par player
        GameObject speBarreTemp = Instantiate(speBarrePrefab, playerInterfaceTempo.transform);
        speBarreTemp.name = "SpéChargeBarre " + (1);
        player.GetComponent<PlayerAttack>().SpeBarreSlider = speBarreTemp.GetComponent<Slider>();
    }
}
