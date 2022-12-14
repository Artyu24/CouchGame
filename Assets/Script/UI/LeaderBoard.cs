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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(RestartGame());
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            PlayerManager.instance.playersSortedByScore[i].transform.position = spawnPoints[i].position;
            PlayerManager.instance.playersSortedByScore[i].transform.rotation = spawnPoints[i].rotation;
            PlayerManager.instance.playersSortedByScore[i].transform.localScale *= taillePingouin;
            PlayerManager.instance.playersSortedByScore[i].GetComponent<PlayerAttack>().EffectSpeBarre.SetActive(false);

            //reset l'anim
        }
        PlayerManager.instance.playersSortedByScore[0].GetComponentInChildren<Animator>().Play("Dance");
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(timeBeforeRestart);
        SceneManager.LoadScene("LobbyV1_Working");
    }
}
