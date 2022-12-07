using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [Tooltip("POint gagner à chaque kill")] 
    public int scoreKill = 10;

    [Tooltip("Multiplicateur de score"), SerializeField]
    private int multiplier;

    private bool addMiddleScore = true;
    public Sprite courroneUI, emptyCourroneUI;
    [SerializeField]
    private GameObject scoreBoard;

    public GameObject hyperSpeed;
    public List<GameObject> terrain = new List<GameObject>();
    
    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;

        scoreTextPrefab = Resources.Load<GameObject>("UI/ScoreText");
    }

    void Start()
    {
        for (int i = 0; i < GameManager.instance.tabCircle.Count; i++)
        {
            terrain.Add(GameManager.instance.tabCircle[i]);
        }

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
        //AddScore(scoreMiddle, GameManager.instance.PlayerInMiddle.GetComponent<Player>());
        //StartCoroutine(CenterPoint.instance.PopPointText());
        CenterPoint.Instance.Test();
        yield return new WaitForSeconds(middelPointsCooldown);
        addMiddleScore = true;
    }

    public void UpdateScores()
    {
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            scorePlayerText[i].text = PlayerManager.instance.players[i].score.ToString();
        }
    }

    public void AddScore(int points, Player player)
    {
        int multi = 1;
        if (player.Multiplier)
            multi = multiplier;

        player.score += points * multi;

        ScoreBoardSorting();
        UpdateScores();
    }

    private void ScoreBoardSorting()
    {
        List<Player> playersSortedByScore = new List<Player>();
        List<RectTransform> playersRankGUISortedByScore = new List<RectTransform>();
        List<Vector3> positionUIScoreInOrder = new List<Vector3>();

        for(int i = 0; i < PlayerManager.instance.players.Count; ++i)
        {
            RectTransform playerRankGUITransform = scoreBoard.GetComponent<Transform>().GetChild(i).GetComponent<RectTransform>();
            positionUIScoreInOrder.Add(playerRankGUITransform.position);
        }

        int bestScore = int.MinValue;
        Player playerWithBestScore = null;
        while (playersSortedByScore.Count < PlayerManager.instance.players.Count)
        {
            foreach (var p in PlayerManager.instance.players)
            {
                if (!playersSortedByScore.Contains(p.Value) && p.Value.score > bestScore)
                {
                    bestScore = p.Value.score;
                    playerWithBestScore = p.Value;
                }
            }
            playersSortedByScore.Add(playerWithBestScore);
            bestScore = int.MinValue;

        }

        for (int i = 0; i < playersSortedByScore.Count; i++)
        {
            playersRankGUISortedByScore.Add(scoreBoard.GetComponent<Transform>().GetChild(playersSortedByScore[i].playerID).GetComponent<RectTransform>());
            //Debug.Log(PlayerListSortByScore[i]);
        }

        for (int i = 0; i < playersRankGUISortedByScore.Count; i++)
        {
            playersRankGUISortedByScore[i].parent = null;
        }

        for (int i = 0; i < positionUIScoreInOrder.Count; i++)
        {
            playersRankGUISortedByScore[i].position = positionUIScoreInOrder[i];
            playersRankGUISortedByScore[i].parent = scoreBoard.transform;
        }

        playersSortedByScore[0].couronne.SetActive(true);
        for (int i = 1; i < playersSortedByScore.Count; i++)
        {
            playersSortedByScore[i].couronne.SetActive(false);
        }

        playersRankGUISortedByScore[0].GetChild(0).GetChild(0).GetComponent<Image>().sprite = courroneUI;
        for (int i = 1; i < playersRankGUISortedByScore.Count; i++)
        {
            playersRankGUISortedByScore[i].GetChild(0).GetChild(0).GetComponent<Image>().sprite = emptyCourroneUI;
        }

    }

    public void InstantiateScoreText(int p) 
    {
        GameObject scoreTextTemp = Instantiate(scoreTextPrefab, PlayerManager.instance.PlayersInterface[p].transform.GetChild(1).transform);//Dangereux si l'ui score change

        scoreTextTemp.name = "Player " + (p + 1);
        scorePlayerText[p] = scoreTextTemp.GetComponent<Text>();
        //Debug.Log(PlayerManager.instance.players[p].GetComponent<Player>().currentColor);
        //scorePlayerText[p].color = PlayerManager.instance.players[p].GetComponent<Player>().currentColor;
        UpdateScores();
    }
}
