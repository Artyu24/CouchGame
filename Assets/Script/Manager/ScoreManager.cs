using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text[] scorePlayerText = new Text[4];
    public GameObject scoreTextPrefab;
    public GameObject scoreParent;

    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;


    }

    void Start()
    {
        for (int p = 0; p < 4 /*GameManager.instance.players.Count*/; p++)
        {
            GameObject temp = Instantiate(scoreTextPrefab, scoreParent.transform);
            scorePlayerText[p] = temp.GetComponent<Text>();
            temp.name = "Player " + (p + 1);
        }
    }

    public void UpdateScores()
    {
        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            scorePlayerText[i].text = "Player " + (i + 1) + " : " + GameManager.instance.players[i+1].score;
        }
    }

    public void AddScore(int points , Player player)
    {
        player.score += points;

        UpdateScores();
    }
}
