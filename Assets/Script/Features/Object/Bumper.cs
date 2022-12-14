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
            FindObjectOfType<AudioManager>().PlayRandom(SoundState.HurtSound);
            FindObjectOfType<AudioManager>().PlayRandom(SoundState.BumperTouchedSound);
            StartCoroutine(PlayAnim());

            if (GetComponent<RailedBumper>().playerTriggeredBy != null)
            {
                if (other.gameObject != GetComponent<RailedBumper>().playerTriggeredBy.gameObject)
                    other.GetComponent<PlayerAttack>().HitTag(GetComponent<RailedBumper>().playerTriggeredBy);
            }
        }

    }

    public IEnumerator PlayAnim()
    {
        m_Animator.SetBool("Bumped", true);
        yield return new WaitForSeconds(0.667f);
        m_Animator.SetBool("Bumped", false);
    }
}
