using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{   
       
 
   public float timeToRespawn;   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {          
            StartCoroutine(other.gameObject.GetComponent<PlayerRespawn>().Die());
        }      
    }
}
