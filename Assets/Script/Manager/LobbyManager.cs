using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private List<GameObject> listOfPlayerToStart = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Check si le player est pas déjà dans la liste
            listOfPlayerToStart.Add(other.gameObject);
            //Bloquer les mouvements du player comme dans l'igloo

            if (listOfPlayerToStart.Count >= PlayerManager.instance.players.Count)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            listOfPlayerToStart.Remove(other.gameObject);
        }
    }
}
