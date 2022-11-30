using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public CapsuleCollider capsulCollider;
    private List<Player> playerList = new List<Player>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 push = (other.transform.position - capsulCollider.transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(push * GameManager.instance.PushForceBumper);
            other.gameObject.GetComponent<Player>().isChockedWaved = true;
            playerList.Add(other.gameObject.GetComponent<Player>());
            int xcount = Random.Range(0, 5);
            FindObjectOfType<AudioManager>().PlayRandom(SoundState.HurtSound);
        }
    }
}
