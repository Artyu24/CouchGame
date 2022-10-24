using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    private float timerBegin;
    private bool scoreWindowIsActive = false;
    [SerializeField] private GameObject scoreWindow;
    [SerializeField] private GameObject textParent;
    private Text[] scorePlayerText = new Text[4];
    public GameObject finalScoreTextPrefab;

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
        if (GameManager.instance.Timer <= 0.0f && !scoreWindowIsActive)
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
        scoreWindowIsActive = true;
        scoreWindow.SetActive(scoreWindowIsActive);
        for (int p = 0; p < GameManager.instance.players.Count; p++)
        {
            GameObject temp = Instantiate(finalScoreTextPrefab, textParent.transform);
            scorePlayerText[p] = temp.GetComponent<Text>();
            temp.name = "Player " + (p + 1);
        }

        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            scorePlayerText[i].text = "Player " + (i + 1) + " : " + GameManager.instance.players[i + 1].score;
        }
    }

    private IEnumerator ReloadScene()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Proto_Vincent");
        yield return new WaitForSeconds(3);
    }
}
