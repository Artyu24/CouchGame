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
    private GameObject[] interfaceUIPrefab = new GameObject[4];
    public GameObject[] InterfaceUiPrefab => interfaceUIPrefab;
    
    private GameObject[] playersInterface = new GameObject[4];
    public GameObject[] PlayersInterface => playersInterface;
    //public List<GameObject> speBarreParentList = new List<GameObject>();
    #endregion
    public Dictionary<int, Player> players = new Dictionary<int, Player>();
    public List<Gamepad> manettes = new List<Gamepad>();


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
        int xcount = Random.Range(0, 3);

        switch (xcount)
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
        }

        int id = players.Count;

        Player dataPlayer = player.GetComponent<Player>();
        dataPlayer.playerID = players.Count;
        dataPlayer.ActualPlayerState = PlayerState.FIGHTING;
        
        //Dict Players
        players.Add(id, dataPlayer);
        
        Init(id, player);
        
        ScoreManager.instance.UpdateScores();
    }

    public void FindCanvas()
    {
        canvasUI = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void Init(int i, GameObject player)
    {
        //Vider les score de manche pour les joueurs
        players[i].score = 0;
        //Spawn at point
        player.transform.position = PointAreaManager.instance.PlayerSpawnStart[i].position;


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
