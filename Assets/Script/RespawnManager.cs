using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    
   public GameObject player;
   [Header("-------Mettre Tout les spawner-------")]
   [SerializeField] Transform[] respawn;

   public float timeToRespawn;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Die());

        }
        
    }

    public IEnumerator Die()
    {
        int ran = Random.Range(0, 8);
        switch (ran)
        {
            case 0:
                
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[0].position;
                break;
            case 1:
                
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[1].position;
                break;
            case 2:
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[2].position;
                break;
            case 3:
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[3].position;
                break;
            case 4:
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[4].position;
                break;
            case 5:
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[5].position;
                break;
            case 6:
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[6].position;
                break;
            case 7:
                yield return new WaitForSeconds(timeToRespawn);
                player.transform.position = respawn[7].position;
                break;
        }
        yield break;
    }
}
