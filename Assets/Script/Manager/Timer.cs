using System;
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

    private float timerCountDown = 5;

    private int minutes, seconds;
    public string levelName;
    #endregion

    #region Scoreboard
    private bool scoreWindowRoundIsActive = false;
    private bool scoreWindowGeneralIsActive = false;

    [SerializeField] private GameObject scoreWindowRound, scoreWindowGeneral;
    [SerializeField] private GameObject textParentRound, textParentGeneral;
    public GameObject roundScoreTextPrefab, generalScoreTextPrefab; 

    private Text[] scorePlayerText = new Text[4];
    private Text[] scoreGeneralPlayerText = new Text[4];

    private GameObject[] medals = new GameObject[3];
    private int numberOfMedal = 3;
    [SerializeField]
    private int pointToWin;
    #endregion

    private void Awake()
    {
        timerText = GetComponent<Text>();
        medals[0] = Resources.Load<GameObject>("UI/GoldMedalParent");
        medals[1] = Resources.Load<GameObject>("UI/SilverMedalParent");
        medals[2] = Resources.Load<GameObject>("UI/CopperMedalParent");
    }

    private void Start()
    {
        PlayerManager.instance.FindCanvas();

        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            Debug.Log("Init");
            PlayerManager.instance.Init(i, PlayerManager.instance.players[i].gameObject);
        }
    }

    void Update()
    {
        if (GameManager.instance.ActualGameState == GameState.INGAME)
        {
            if (GameManager.instance.Timer <= 0.0f && !scoreWindowRoundIsActive)
            {
                PrintScoreWindow();
                //PrintGeneralScoreWindow();
            }

            if (!scoreWindowRoundIsActive)
            {
                GameManager.instance.Timer -= Time.deltaTime;
                minutes = Mathf.FloorToInt(GameManager.instance.Timer / 60f);
                seconds = Mathf.FloorToInt(GameManager.instance.Timer % 60f);
            }

            if (GameManager.instance.Timer >= 0.0f)
            {
                timerText.text = minutes.ToString("00") + " : " + seconds.ToString("00");
            }
            else
            {
                timerText.text = "00 : 00";
            }
        }
        else if (GameManager.instance.ActualGameState == GameState.INIT)
        {
            if (timerCountDown <= 0)
            {
                CameraManager.Instance.ChangeCamera();
                GameManager.instance.ActualGameState = GameState.INGAME;
            }

            timerCountDown -= Time.deltaTime;
            seconds = Mathf.FloorToInt(timerCountDown % 60f);
            
            if (timerCountDown >= 0.0f)
            {
                timerText.text = seconds.ToString("00");
            }
            else
            {
                timerText.text = "0";
            }
        }
        else
        {
            timerText.text = "WHEN STARTING BRO";
        }
    }
    private void PrintScoreWindow()
    {
        scoreWindowRoundIsActive = true;
        Time.timeScale = 0;
        scoreWindowRound.SetActive(scoreWindowRoundIsActive);

        for (int p = 0; p < PlayerManager.instance.players.Count; p++)
        {
            GameObject temp = Instantiate(roundScoreTextPrefab, textParentRound.transform);
            scorePlayerText[p] = temp.GetComponentInChildren<Text>();
            temp.name = "Player " + (p + 1);
        }

        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            Debug.Log(PlayerManager.instance.players[i].score);
            scorePlayerText[i].text = "Player " + (i + 1) + " : " + PlayerManager.instance.players[i].score;
        }
    }

    public void PrintGeneralScoreWindow()
    {
        scoreWindowRoundIsActive = false;
        scoreWindowRound.SetActive(scoreWindowRoundIsActive);
        
        if (!scoreWindowGeneralIsActive)
        {
            scoreWindowGeneralIsActive = true;
            scoreWindowGeneral.SetActive(scoreWindowGeneralIsActive);
            List<Player> tempPlayerListPlayer = new List<Player>();
            int position = 0;

            Player playerTemp = null;
            int bestScore = 0;

            // Rangage des joueurs par score
            while (tempPlayerListPlayer.Count < PlayerManager.instance.players.Count)
            {
                foreach (var player in PlayerManager.instance.players)
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
                temp.name = "Player " + (tempPlayerListPlayer[p].playerID + 1);
                scoreGeneralPlayerText[p].text = "Player " + (tempPlayerListPlayer[p].playerID + 1) + " : ";

                // Spawn des nouvelles medailes pour chaque joueurs en fonction de leur classement
                for (int i = 0; i < PlayerManager.instance.players[p].scoreGeneral; i++)
                {
                    GameObject test = Instantiate(PlayerManager.instance.players[p].medals[p], temp.transform);
                }
                for (int i = 0; i < numberOfMedal; i++)
                {
                    if (tempPlayerListPlayer[p].score > 0)
                    {
                        //Anim d'apparition
                        InstantiateMedals(temp.transform, position);
                        PlayerManager.instance.players[p].scoreGeneral++;
                    }
                }
                position++;
                numberOfMedal--;
            }
            for (int i = 0; i < PlayerManager.instance.players.Count; i++)
            {
                if (PlayerManager.instance.players[i].medals.Count >= pointToWin)
                {
                    Debug.Log("WE HAVE A WINNER !!!");
                }
            }
            
        }
    }
    private void InstantiateMedals(Transform t, int position)
    {
        GameObject temp2 = Instantiate(medals[Mathf.Abs(position)], t);
        temp2. GetComponentInChildren<Animator>().SetTrigger("SpawnMedal");
        PlayerManager.instance.players[position].medals.Add(medals[Mathf.Abs(position)]);
    }

    public void ReloadScene()
    {
        Array.Clear(PlayerManager.instance.PlayersInterface, 0, PlayerManager.instance.PlayersInterface.Length);
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }
}
