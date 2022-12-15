using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    public static LeaderBoard Instance;
    [SerializeField]
    private float taillePingouin = 2.5f;
    [SerializeField]
    private float timeBeforeRestart = 15;
    List<Player> playersSortedByScore = new List<Player>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        int bestScore = int.MinValue;
        Player playerWithBestScore = null;

        while (playersSortedByScore.Count < PlayerManager.instance.players.Count)
        {
            foreach (var p in PlayerManager.instance.players)
            {
                if (!playersSortedByScore.Contains(p.Value) && p.Value.medals.Count > bestScore)
                {
                    bestScore = p.Value.medals.Count;
                    playerWithBestScore = p.Value;
                }
            }
            playersSortedByScore.Add(playerWithBestScore);
            bestScore = int.MinValue;
        }
    }

    void Start()
    {
        StartCoroutine(RestartGame());
        
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            playersSortedByScore[i].transform.position = spawnPoints[i].position;
            playersSortedByScore[i].transform.rotation = spawnPoints[i].rotation;
            playersSortedByScore[i].transform.localScale *= taillePingouin;
            playersSortedByScore[i].GetComponent<PlayerAttack>().EffectSpeBarre.SetActive(false);
        }
        playersSortedByScore[0].GetComponentInChildren<Animator>().Play("Dance");
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(timeBeforeRestart);
        SceneManager.LoadScene("LobbyV1_Working");
    }
}
