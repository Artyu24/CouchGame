using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public CapsuleCollider capsulCollider;
    private List<Player> playerList = new List<Player>();
    public Animator m_Animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 push = (other.transform.position - capsulCollider.transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(push * GameManager.instance.PushForceBumper);
            other.gameObject.GetComponent<Player>().isChockedWaved = true;
            playerList.Add(other.gameObject.GetComponent<Player>());
            FindObjectOfType<AudioManager>().PlayRandom(SoundState.HurtSound);
            StartCoroutine(PlayAnim());
        }
    }

    public IEnumerator PlayAnim()
    {
        m_Animator.SetBool("Bumped", true);
        yield return new WaitForSeconds(0.583f);
        m_Animator.SetBool("Bumped", false);
    }
}
