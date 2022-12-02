using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IInteractable
{
    [SerializeField] SphereCollider sphereCollider;

    private GameObject playerTriggeredBy;

    [SerializeField] private bool speedUpAnim = false;
    private float speedFactor = 1;
    public bool isGrounded;

    // Start is called before the first frame update
    void Update()
    {
        if(speedUpAnim)
            GetComponent<Animator>().SetFloat("DetonateSpeed", speedFactor); speedFactor += Time.deltaTime;

        if(isGrounded)
            GetComponent<Rigidbody>().velocity = Vector3.zero;

    }

    public void Interact(Player player)
    {
        Debug.Log(player);
        StartCoroutine(Explosion(player.gameObject));
    }
   
    IEnumerator Explosion(GameObject _player)
    {
        //Debug.LogError("BOOOOOOOOOOOM");

        GetComponent<Animator>().SetTrigger("Boom");
        speedUpAnim = true; 

        playerTriggeredBy = _player;


        yield return new WaitForSecondsRealtime(ObjectManager.Instance.timeBeforeExplosion);

        sphereCollider.gameObject.SetActive(true); 

        GetComponent<SphereCollider>().enabled = true;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        //trigger Particle effect
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Rigidbody>().useGravity = false;
        //trigger Sound

        yield return new WaitForSecondsRealtime(1.0f);

        Destroy(gameObject);
    }

    public void OnTriggerStay(Collider hit)
    {
        if (hit.transform != null && hit.transform.tag == "Player")
        {
            PlayerAttack player = hit.GetComponent<PlayerAttack>();

            Vector3 dir = new Vector3((hit.transform.position.x - transform.position.x), 3, (hit.transform.position.z - transform.position.z)).normalized;
            hit.attachedRigidbody.AddForce(dir * ObjectManager.Instance.bombStrenght);

            if (hit.gameObject != playerTriggeredBy.gameObject)
                player.HitTag(playerTriggeredBy);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Platform")
            StartCoroutine(stopVelocity());
    }

    IEnumerator stopVelocity()
    {
        yield return new WaitForSecondsRealtime(.3f);
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Platform")
            isGrounded = false;
    }

}
