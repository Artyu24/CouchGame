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

    private void Start()
    {
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            PlayerManager.instance.players[i].transform.localScale = new Vector3(1,1,1);
        }
        
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
                    player.GetComponent<Player>().ActualPlayerState = PlayerState.WAIT;
                }
                StartCoroutine(LoadNextScene());
            }
        }
    }

    private IEnumerator LoadNextScene()
    {
        CameraManager.Instance.AnimTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        foreach (GameObject player in ListOfPlayerToStart)
        {
            player.GetComponent<Player>().HideGuy(true);
            player.GetComponent<Player>().ActualPlayerState = PlayerState.FIGHTING;
            player.GetComponent<PlayerMovement>().MovementInput = Vector3.zero;
        }


        SceneManager.LoadScene(1);
    }
}
