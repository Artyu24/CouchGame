using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLobby : MonoBehaviour
{
    private List<GameObject> listOfPlayerToSettings = new List<GameObject>();
    public List<GameObject> ListOfPlayerToSettings => listOfPlayerToSettings;

    public static SettingsLobby instance;

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
            listOfPlayerToSettings.Add(other.gameObject);
            other.GetComponent<Player>().ActualPlayerState = PlayerState.WAITINGSETTINGS;

            //Bloquer les mouvements du player comme dans l'igloo
            other.GetComponent<Player>().HideGuy(false);

            if (listOfPlayerToSettings.Count >= PlayerManager.instance.players.Count)
            {
                Debug.Log("Settings ouvre toi");
            }
        }
    }
}
