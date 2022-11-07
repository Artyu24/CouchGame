using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region UI
    public GameObject speBarrePrefab;
    public List<GameObject> speBarreParentList = new List<GameObject>();
    #endregion
    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    public List<Gamepad> manettes = new List<Gamepad>();
    [SerializeField] private Transform[] spawnList = new Transform[] { };
    public Transform[] SpawnList => spawnList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("LostPlayer");
        manettes.Add(Gamepad.current);
        Player dataPlayer = player.GetComponent<Player>();
        players.Add(players.Count + 1, dataPlayer);
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;

        Debug.Log("Players : " + players.Count);
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
