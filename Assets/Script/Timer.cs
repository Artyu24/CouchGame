using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Timer
    private Text timerText;
    private Text timerSet;
    private float timerBegin;
    private int minutes, seconds;
    #endregion

    #region Scoreboard
    private bool scoreWindowRoundIsActive = false;
    private bool scoreWindowGeneralIsActive = false;
    [SerializeField] private GameObject scoreWindowRound, scoreWindowGeneral;
    [SerializeField] private GameObject textParentRound, textParentGeneral;
    private Text[] scorePlayerText = new Text[4];
    private Text[] scoreGeneralPlayerText = new Text[4];
    public GameObject roundScoreTextPrefab, generalScoreTextPrefab;
    public List<GameObject> medals = new List<GameObject>();
    //private int nbrOfRound = 0;
    #endregion

    private void Awake()
    {
        timerText = GetComponent<Text>();
    }

    private void Start()
    {
        timerBegin = GameManager.instance.Timer;
    }

    void Update()
    {
        if (GameManager.instance.Timer <= 0.0f && !scoreWindowRoundIsActive)
        {
            PrintScoreWindow();
        }

        if (GameManager.instance.players.Count >= 1 && !scoreWindowRoundIsActive)
        {
            GameManager.instance.Timer -= Time.deltaTime;
            minutes = Mathf.FloorToInt(GameManager.instance.Timer / 60f);
            seconds = Mathf.FloorToInt(GameManager.instance.Timer % 60f);
        }

        timerText.text = minutes.ToString("00") + " : " + seconds.ToString("00");
    }

    private void PrintScoreWindow()
    {
        scoreWindowRoundIsActive = true;
        Time.timeScale = 0;
        scoreWindowRound.SetActive(scoreWindowRoundIsActive);

        for (int p = 0; p < GameManager.instance.players.Count; p++)
        {
            GameObject temp = Instantiate(roundScoreTextPrefab, textParentRound.transform);
            scorePlayerText[p] = temp.GetComponent<Text>();
            temp.name = "Player " + (p + 1);
        }

        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            scorePlayerText[i].text = "Player " + (i + 1) + " : " + GameManager.instance.players[i + 1].score;
        }
    }

    public void PrintGeneralScoreWindow()
    {
        scoreWindowRoundIsActive = false;
        scoreWindowRound.SetActive(scoreWindowRoundIsActive);
        scoreWindowGeneralIsActive = true;
        scoreWindowGeneral.SetActive(scoreWindowGeneralIsActive);
        
        List<Player> tempPlayerListPlayer = new List<Player>();
        int position = 0;

        Player playerTemp = null;
        int bestScore = 0;
        while (tempPlayerListPlayer.Count < GameManager.instance.players.Count)
        {
            foreach (var player in GameManager.instance.players)
            {
                if (!tempPlayerListPlayer.Contains(player.Value) && bestScore <= player.Value.score)
                {
                    bestScore = player.Value.score;
                    playerTemp = player.Value;
                }
            }
            tempPlayerListPlayer.Add(playerTemp);
            bestScore = 0;
        }
        
        GameObject temp = null;

        for (int p = 0; p < tempPlayerListPlayer.Count; p++)
        {
            temp = Instantiate(generalScoreTextPrefab, textParentGeneral.transform);
            scoreGeneralPlayerText[p] = temp.GetComponentInChildren<Text>();
            temp.name = "Player " + tempPlayerListPlayer[p].playerID;
            scoreGeneralPlayerText[p].text = "Player " + tempPlayerListPlayer[p].playerID + " : ";

            for (int i = 0; i < (tempPlayerListPlayer.Count-p+1); i++)
            {
                if (tempPlayerListPlayer[p].score > 0)
                {
                    InstantiateMedals(temp.transform, position);
                    GameManager.instance.players[p + 1].scoreGeneral++;
                }
                /*if (nbrOfRound >= 1)
                {
                    for (int j = 0; j < GameManager.instance.players[p + 1].scoreGeneral; j++)
                    {
                        InstantiateMedals(temp.transform, position);
                    }
                }*/
            }

            position++;
        }

        //nbrOfRound++;
    }

    private void InstantiateMedals(Transform t, int position)
    {
        GameObject temp2 = Instantiate(medals[Mathf.Abs(position)], t);

    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Proto_Téo");
    }
}
