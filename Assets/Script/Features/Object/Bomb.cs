using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IInteractable
{
    [SerializeField] SphereCollider sphereCollider;

    private GameObject playerTriggeredBy;
    public GameObject PlayerTriggeredBy
    {
        get => playerTriggeredBy;
        set => playerTriggeredBy = value;
    }

    private bool speedUpAnim = false;

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void test()
    {
        StartCoroutine(Explosion());

    }

    // Start is called before the first frame update
    void Start()
    {
        if(speedUpAnim)
            GetComponent<Animator>().SetFloat("DetonateSpeed", Time.deltaTime);
    }

    IEnumerator Explosion()
    {
        GetComponent<Animator>().SetTrigger("Boom");
        speedUpAnim = true; 

        yield return new WaitForSecondsRealtime(ObjectManager.Instance.timeBeforeExplosion);

        sphereCollider.gameObject.SetActive(true);
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
        }
        //trigger Particle effect
        //trigger Sound

        yield return new WaitForSecondsRealtime(2.0f);

        Debug.Log(playerTriggeredBy);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider hit)
    {
        if (hit.transform != null && hit.transform.tag == "Player")
        {
            Player player = hit.GetComponent<Player>();
            player.ActualPlayerState = PlayerState.DEAD;
            player.Kill();

            ScoreManager.instance.AddScore(ScoreManager.instance.scoreKill, playerTriggeredBy.GetComponent<Player>());
        }
    }
}
