using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocWave : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public float radiusMax;
    public float growingSpeed;
    //public GameObject player;
    public float pushForce;
    bool getPushed = false;
    public Transform transparence;
    private List<Player> playerList = new List<Player>();

    private void Start()
    {        
        transparence.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if(transparence.localScale.x < radiusMax)
        {            
            transparence.localScale = new Vector3(transparence.localScale.x + Time.deltaTime * growingSpeed, transparence.localScale.y + Time.deltaTime * growingSpeed, transparence.localScale.z + Time.deltaTime * growingSpeed);
        }


        if(transparence.localScale.x >= radiusMax)
        {
            getPushed = false;
            foreach(Player player in playerList)
            {
                player.isChockedWaved = false;                
            }
            playerList.Clear();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(getPushed == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<Player>().isChockedWaved == false && other.gameObject.GetComponent<Player>().isInvincible == false)
                {
                    Vector3 push = (other.transform.position - sphereCollider.transform.position).normalized;
                    other.GetComponent<Rigidbody>().AddForce(push * pushForce);
                    other.gameObject.GetComponent<Player>().isChockedWaved = true;
                    playerList.Add(other.gameObject.GetComponent<Player>());
                }
            }
        }
        
    }
}
