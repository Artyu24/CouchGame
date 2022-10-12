using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("-------Mettre Tout les spawner-------")]
    [SerializeField]
    Transform[] respawn;    
    public float timeToRespawn;
    
    
    public void SpawnPlayer()
    {
            //this.gameObject.SetActive(true);
            Debug.Log("on est la ");
            int ran = Random.Range(0, 8);
            switch (ran)
            {
                case 0:
                    this.gameObject.GetComponent<Transform>().position = respawn[0].transform.position;
                    
                break;
                case 1:
                    this.gameObject.GetComponent<Transform>().position = respawn[1].transform.position;
                    break;
                case 2:
                    this.gameObject.GetComponent<Transform>().position = respawn[2].transform.position;
                    break;
                case 3:
                    this.gameObject.GetComponent<Transform>().position = respawn[3].transform.position;
                    break;
                case 4:
                    this.gameObject.GetComponent<Transform>().position = respawn[4].transform.position;
                    break;
                case 5:
                    this.gameObject.GetComponent<Transform>().position = respawn[5].transform.position;
                    break;
                case 6:
                    this.gameObject.GetComponent<Transform>().position = respawn[6].transform.position;
                    break;
                case 7:
                    this.gameObject.GetComponent<Transform>().position = respawn[7].transform.position;
                    break;
            }        
    }

    public IEnumerator Die()
    {
        
        //gameObject.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        SpawnPlayer();    

    }
}
