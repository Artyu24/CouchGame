using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private GameObject[] playerUiInterface = new GameObject[4];

    public GameObject ui;

    public Transform[] SpawnList => spawnList;
    public GameObject[] PlayerUiInterface => playerUiInterface;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        speBarrePrefab = Resources.Load<GameObject>("UI/SpeChargeBarre");
        playerUiInterface = Resources.LoadAll<GameObject>("UI/PlayersUI");
        ui = GameObject.FindGameObjectWithTag("Canvas");
        //Debug.Log("Player count : " + players.Count);
    }

    public void AddPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("LostPlayer");
        manettes.Add(Gamepad.current);
        Player dataPlayer = player.GetComponent<Player>();
        players.Add(players.Count + 1, dataPlayer);
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;
        Init(dataPlayer.playerID - 1, player);
        player.tag = "Player";
        ScoreManager.instance.UpdateScores();
    }

    public Transform RandomSpawn()
    {
        int random = Random.Range(0, spawnList.Length);
        return spawnList[random];
    }

    public void Init(int i, GameObject player)
    {
        //player.transform.position = spawnList[i].position;
        GameObject playerInterface = Instantiate(playerUiInterface[i], ui.transform);
        playerUiInterface[i] = playerInterface;
        ScoreManager.instance.InstantiateScoreText(i);
        GameObject temp = Instantiate(speBarrePrefab, playerInterface.transform);
        //speBarrePrefab = temp;
        temp.name = "SpéChargeBarre " + (1);
    }
}
