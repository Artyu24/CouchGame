using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    private Text timerSet;
    private float timerBegin;
    private int minutes, seconds;

    private bool scoreWindowRoundIsActive = false;
    private bool scoreWindowGeneralIsActive = false;
    [SerializeField] private GameObject scoreWindowRound, scoreWindowGeneral;
    [SerializeField] private GameObject textParentRound, textParentGeneral;
    private Text[] scorePlayerText = new Text[4];
    private Text[] scoreGeneralPlayerText = new Text[4];
    public GameObject roundScoreTextPrefab, generalScoreTextPrefab, goldMedalIcon, silverMedalIcon, copperMedalIcon;

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

        GameObject temp = null;

        //Mettre dans l'ordre les joueurs par points
        //Idee : creer un tableau/liste qui va accueillir les joueurs dans le bonne ordre
        //c'est celui la qui va etre utilisé pour afficher les joueurs sur score generale
        /*for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            Mathf.Max(GameManager.instance.players[i].score, GameManager.instance.players[i + 1].score);
        }*/

        for (int p = 0; p < GameManager.instance.players.Count; p++)
        {
            temp = Instantiate(generalScoreTextPrefab, textParentGeneral.transform);
            scoreGeneralPlayerText[p] = temp.GetComponent<Text>();
            temp.name = "Player " + (p + 1);

            for(int i = 0; i < GameManager.instance.players.Count; i++)
            {
                for (int j = 0; j < (GameManager.instance.players[p+1].score/10); j++)
                {
                    GameObject temp2 = Instantiate(goldMedalIcon, temp.transform);
                    GameManager.instance.players[p + 1].scoreGeneral++;
                }
            }
        }
    }

    private IEnumerator ReloadScene()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Proto_Vincent");
        yield return new WaitForSeconds(3);
    }
}
