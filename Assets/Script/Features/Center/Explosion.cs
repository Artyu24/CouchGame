using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SphereCollider sphereCollider;
    bool getPushed = false;
    private List<Player> playerList = new List<Player>();

    private void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (transform.localScale.x < GameManager.instance.RadiusMaxExplosion)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * GameManager.instance.GrowingSpeedExplosion, transform.localScale.y + Time.deltaTime * GameManager.instance.GrowingSpeedExplosion, transform.localScale.z + Time.deltaTime * GameManager.instance.GrowingSpeedExplosion);
        }


        if (transform.localScale.x >= GameManager.instance.RadiusMaxExplosion)
        {
            getPushed = false;
            foreach (Player player in playerList)
            {
                player.isChockedWaved = false;
            }
            playerList.Clear();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (getPushed == false)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<Player>().isChockedWaved == false && other.gameObject.GetComponent<Player>().isInvincible == false)
                {
                    Vector3 push = (other.transform.position - sphereCollider.transform.position).normalized;
                    other.GetComponent<Rigidbody>().AddForce(push * GameManager.instance.PushForceExplosion);
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
