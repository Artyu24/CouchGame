using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int scoreJ1, scoreJ2, scoreJ3, scoreJ4;
    public Text scoreJ1Text, scoreJ2Text, scoreJ3Text, scoreJ4Text;
    public static ScoreManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        scoreJ1Text.text = ("Joueur 1 :" + scoreJ1.ToString());
        scoreJ2Text.text = ("Joueur 2 :" + scoreJ2.ToString());
        scoreJ3Text.text = ("Joueur 3 :" + scoreJ3.ToString());
        scoreJ4Text.text = ("Joueur 4 :" + scoreJ4.ToString());
        
    }

    public void AddScore(int points , Player player)
    {
        
    }
}
