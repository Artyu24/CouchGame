using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitLobby : MonoBehaviour
{
    private List<GameObject> listOfPlayerToQuit = new List<GameObject>();
    public List<GameObject> ListOfPlayerToQuit => listOfPlayerToQuit;

    public static QuitLobby instance;

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
            listOfPlayerToQuit.Add(other.gameObject);
            other.GetComponent<Player>().ActualPlayerState = PlayerState.WAITINGQUIT;

            //Bloquer les mouvements du player comme dans l'igloo
            other.GetComponent<Player>().HideGuy(false);

            if (listOfPlayerToQuit.Count >= PlayerManager.instance.players.Count)
            {
                Application.Quit();
            }
        }
    }
}
