using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public Text[] scorePlayerText = new Text[4];
    [HideInInspector]
    public GameObject scoreTextPrefab;
    [Tooltip("Point gagner par le joueur est au milieu")]
    public int scoreMiddle = 10;
    [Tooltip("Point gagner par le joueur lorsqu'il marche sur un interrupteur")]
    public int scoreInterrupteur = 5;
    [Tooltip("Temps entre 2 gain de point que le joueur est au milieu")]
    public float middelPointsCooldown = 2;
    [Tooltip("Point gagner à chaque hit du sac")] 
    public int scorePointArea = 1;
    [Tooltip("Point gagner à chaque hit du sac si la sardine est dorée")]
    public int scoreGoldPointArea = 5;
    [Tooltip("POint gagner à chaque kill")] public int scoreKill = 10;
    private bool addMiddleScore = true;

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;

        scoreTextPrefab = Resources.Load<GameObject>("UI/ScoreText");
    }

    void Start()
    {
        if(scorePlayerText == null || scoreTextPrefab == null)
            return;

        if (scoreTextPrefab == null || PlayerManager.instance.InterfaceUiPrefab == null)
        {
            Debug.Log("La liste de parent pour les score est vide OU le prefab de score est vide !");
        }
    }

    void Update()
    {
        if (scorePlayerText == null || scoreTextPrefab == null)
            return;
        
        if (GameManager.instance.PlayerInMiddle != null && addMiddleScore)
        {
            StartCoroutine(AddScoreFromMiddle());
        }
    }

    private IEnumerator AddScoreFromMiddle()
    {
        addMiddleScore = false;
        AddScore(scoreMiddle, GameManager.instance.PlayerInMiddle.GetComponent<Player>());
        yield return new WaitForSeconds(middelPointsCooldown);
        addMiddleScore = true;
    }

    public void UpdateScores()
    {
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            scorePlayerText[i].text = /*"Player " + (i + 1) + " : " + */PlayerManager.instance.players[i].score.ToString();
        }
    }

    public void AddScore(int points, Player player)
    {
        player.score += points;

        List<Player> tempPlayerListPlayer = new List<Player>();
        Player playerTemp = null;
        int bestScore = 0;

        while (tempPlayerListPlayer.Count < PlayerManager.instance.players.Count)
        {
            foreach (var p in PlayerManager.instance.players)
            {
                if (!tempPlayerListPlayer.Contains(p.Value) && bestScore <= p.Value.score)
                {
                    bestScore = p.Value.score;
                    playerTemp = p.Value;
                }
            }
            tempPlayerListPlayer.Add(playerTemp);
            bestScore = 0;

        }
        tempPlayerListPlayer[0].couronne.SetActive(true);
        for (int i = 1; i < tempPlayerListPlayer.Count; i++)
        {
            tempPlayerListPlayer[i].couronne.SetActive(false);
        }

        UpdateScores();
    }

    public void InstantiateScoreText(int p) 
    {
        GameObject scoreTextTemp = Instantiate(scoreTextPrefab, PlayerManager.instance.PlayersInterface[p].transform.GetChild(1).transform);//Dangereux si l'ui score change

        scoreTextTemp.name = "Player " + (p + 1);
        scorePlayerText[p] = scoreTextTemp.GetComponent<Text>();
        UpdateScores();
    }
}
