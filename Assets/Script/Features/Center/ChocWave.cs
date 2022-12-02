using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocWave : MonoBehaviour
{
    public SphereCollider sphereCollider;
    bool getPushed = false;
    public Transform transparence;
    private List<Player> playerList = new List<Player>();

    private void Start()
    {        
        transparence.localScale = new Vector3(0, 0, 0);
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.ChocWaveSound);

    }

    private void Update()
    {
        if(transparence.localScale.x < GameManager.instance.RadiusMax)
        {            
            transparence.localScale = new Vector3(transparence.localScale.x + Time.deltaTime * GameManager.instance.GrowingSpeed, transparence.localScale.y + Time.deltaTime * GameManager.instance.GrowingSpeed, transparence.localScale.z + Time.deltaTime * GameManager.instance.GrowingSpeed);
        }


        if(transparence.localScale.x >= GameManager.instance.RadiusMax)
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
                    other.GetComponent<Rigidbody>().AddForce(push * GameManager.instance.PushForce);
                    other.gameObject.GetComponent<Player>().isChockedWaved = true;
                    playerList.Add(other.gameObject.GetComponent<Player>());
                    int xcount = Random.Range(0, 5);
                    FindObjectOfType<AudioManager>().PlayRandom(SoundState.HurtSound);
                    FindObjectOfType<AudioManager>().PlayRandom(SoundState.HitSound);


                }
            }
        }
        
    }
}
