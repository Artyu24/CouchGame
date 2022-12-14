using DG.Tweening;
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
    [Tooltip("POint gagner à chaque kill")] 
    public int scoreKill = 10;

    [Tooltip("Multiplicateur de score"), SerializeField]
    private int multiplier;

    private bool addMiddleScore = true;
    public Sprite courroneUI, emptyCourroneUI;
    [SerializeField]
    private GameObject scoreBoard;
    Sequence swapPlayerSequence;

    private List<Vector3> positionUIScoreInOrder = new List<Vector3>();

    public GameObject hyperSpeed;
    public List<GameObject> terrain = new List<GameObject>();

    [SerializeField]
    private float speedTweenSwap = 1.0f;

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
        CenterPoint.Instance.PopPoint();
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

    public void UpdateScoresPlayer(int id)
    {
        scorePlayerText[id].text = PlayerManager.instance.players[id].score.ToString();
    }

    public void AddScore(int points, Player player)
    {
        int multi = 1;
        if (player.Multiplier)
            multi = multiplier;

        player.score += points * multi;

        player.GetComponent<PlayerAttack>().SpeBarreSlider.transform.parent.transform.GetChild(0).transform.DOPunchScale(Vector3.one,.2f);

        ScoreBoardSorting();
        UpdateScores();
    }

    private void ScoreBoardSorting()
    {
        if (positionUIScoreInOrder.Count == 0)
        {
            for (int i = 0; i < PlayerManager.instance.players.Count; ++i)
            {
                RectTransform playerRankGUITransform = scoreBoard.GetComponent<Transform>().GetChild(i).GetComponent<RectTransform>();
                positionUIScoreInOrder.Add(playerRankGUITransform.position);
            }
        }

        int bestScore = int.MinValue;
        Player playerWithBestScore = null;
        List<Player> playersSortedByScore = new List<Player>(); //list des players trié par score
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

        List<RectTransform> playersRankGUISortedByScore = new List<RectTransform>(); //list des UI (score/speBarre/pp)
        PlayerUIInfo[] playerUIInfos = scoreBoard.GetComponentsInChildren<PlayerUIInfo>();
        for (int i = 0; i < playersSortedByScore.Count; i++)
        {
            foreach (var playerUIInfo in playerUIInfos)
            {
                if (playerUIInfo.PlayerID == playersSortedByScore[i].playerID)
                {
                    playersRankGUISortedByScore.Add(playerUIInfo.GetComponent<RectTransform>());
                }
            }
        }

        scoreBoard.GetComponent<VerticalLayoutGroup>().enabled = false;

        if (swapPlayerSequence != null && swapPlayerSequence.IsPlaying())
        {
            swapPlayerSequence.Kill();
        }
        swapPlayerSequence = DOTween.Sequence();

        for (int i = 0; i < positionUIScoreInOrder.Count; i++)
        {
            float t = ((positionUIScoreInOrder[i] - playersRankGUISortedByScore[i].position).magnitude / speedTweenSwap);
            var tween = playersRankGUISortedByScore[i].DOMove(positionUIScoreInOrder[i], t); 

            if (i == 0)
            {
                swapPlayerSequence.Append(tween);
            } else
            {
                swapPlayerSequence.Join(tween);
            }
        }
        swapPlayerSequence.onComplete += () =>
        {
            //nouvelle sequence shakeFirstPlayer
            //Sequence shakeFirstPlayer = DOTween.Sequence();
            //shakeFirstPlayer.Append(playersRankGUISortedByScore[0].DORotate());
            
            for (int i = 0; i < playersRankGUISortedByScore.Count; i++)
            {
                playersRankGUISortedByScore[i].SetParent(null);
            }
            for (int i = 0; i < playersRankGUISortedByScore.Count; i++)
            {
                playersRankGUISortedByScore[i].SetParent(scoreBoard.transform);
            }
            scoreBoard.GetComponent<VerticalLayoutGroup>().enabled = true;
        };

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
        PlayerManager.instance.playersSortedByScore= playersSortedByScore;
    }

    public void InstantiateScoreText(int p) 
    {
        GameObject scoreTextTemp = Instantiate(scoreTextPrefab, PlayerManager.instance.PlayersInterface[p].transform.GetChild(1).transform);//Dangereux si l'ui score change

        scoreTextTemp.name = "Player " + (p + 1);
        scorePlayerText[p] = scoreTextTemp.GetComponent<Text>();
        //Debug.Log(PlayerManager.instance.players[p].GetComponent<Player>().currentColor);
        //scorePlayerText[p].color = PlayerManager.instance.players[p].GetComponent<Player>().currentColor;
        UpdateScoresPlayer(p);
    }
}
