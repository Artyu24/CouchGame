using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private List<GameObject> listOfPlayerToStart = new List<GameObject>();
    public List<GameObject> ListOfPlayerToStart => listOfPlayerToStart;

    public static LobbyManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Check si le player est pas déjà dans la liste
            listOfPlayerToStart.Add(other.gameObject);
            other.GetComponent<Player>().ActualPlayerState = PlayerState.WAITINGPLAY;

            //Bloquer les mouvements du player comme dans l'igloo
            other.GetComponent<Player>().HideGuy(false);

            if (listOfPlayerToStart.Count >= PlayerManager.instance.players.Count && PlayerManager.instance.players.Count >= 2)
            {
                foreach (GameObject player in ListOfPlayerToStart)
                {
                    player.GetComponent<Player>().HideGuy(true);
                    player.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
                }

                SceneManager.LoadScene(1);
            }
        }
    }
}
