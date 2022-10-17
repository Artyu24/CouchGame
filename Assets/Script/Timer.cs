using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    private float timerBegin;

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
        if (GameManager.instance.Timer <= 0.0f)
        {
            StartCoroutine(ReloadScene());
            Time.timeScale = 1;
        }
        GameManager.instance.Timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(GameManager.instance.Timer / 60f);
        int seconds = Mathf.FloorToInt(GameManager.instance.Timer % 60f);
        
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private IEnumerator ReloadScene()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Proto_Vincent");
        yield return new WaitForSeconds(3);
    }
}
