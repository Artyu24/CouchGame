using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    private float timerBegin;
    private bool scoreWindowRoundIsActive = false;
    private bool scoreWindowGeneralIsActive = false;
    [SerializeField] private GameObject scoreWindowRound, scoreWindowGeneral;
    [SerializeField] private GameObject textParentRound, textParentGeneral;
    private Text[] scorePlayerText = new Text[4];
    public GameObject roundScoreTextPrefab, generalScoreTextPrefab;

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
            Time.timeScale = 0;
            PrintScoreWindow();
        }
        GameManager.instance.Timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(GameManager.instance.Timer / 60f);
        int seconds = Mathf.FloorToInt(GameManager.instance.Timer % 60f);
        
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void PrintScoreWindow()
    {
        scoreWindowRoundIsActive = true;
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

    private void PrintGeneralScoreWindow()
    {
        scoreWindowRoundIsActive = false;
        scoreWindowRound.SetActive(scoreWindowRoundIsActive);
        scoreWindowGeneralIsActive = true;
        scoreWindowGeneral.SetActive(scoreWindowGeneralIsActive);
        for (int p = 0; p < GameManager.instance.players.Count; p++)
        {
            GameObject temp = Instantiate(generalScoreTextPrefab, textParentGeneral.transform);
            scorePlayerText[p] = temp.GetComponent<Text>();
            temp.name = "Player " + (p + 1);
        }




        /*for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            scorePlayerText[i].text = "Player " + (i + 1) + " : " + GameManager.instance.players[i + 1].score;
        }*/
    }

    private IEnumerator ReloadScene()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Proto_Vincent");
        yield return new WaitForSeconds(3);
    }
}
