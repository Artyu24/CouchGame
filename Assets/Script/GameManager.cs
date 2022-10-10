using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] tabCicle;
    public GameObject[] TabCicle => tabCicle;

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
        if (dataPlayer.playerID == 1)
        {
            //player.transform.position = spawnJ1.position;
            //player.name = PseudoManager.pseudoJ1;
            //boxTutoJ1Text.SetActive(false);
            //boxTutoJ2Text.SetActive(true);
        }
        else
        {
            //player.transform.position = spawnJ2.position;
            //player.name = PseudoManager.pseudoJ2;
            //boxTutoJ2Text.SetActive(false);
        }
        player.tag = "Player";
    }
}
