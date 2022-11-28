using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBag : MonoBehaviour
{
    [SerializeField] public int hp = 1;
    private Animator animator;
    public Transform[] fish;

    private GameObject fishToUI;
    public bool isGolden;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        fish = new Transform[transform.GetChild(0).childCount];
        int children = transform.GetChild(0).childCount;
        for (int i = 0; i < children; ++i)
            fish[i] = transform.GetChild(0).GetChild(i);
        hp = 1;
        fishToUI = Resources.Load("UI/FishToUI") as GameObject;
        //Debug.Log(fishToUI);
        if (isGolden)
        {
            GetComponentInChildren<Renderer>().material = Resources.Load("GoldenFish") as Material; ;
        } 
    }

    public void Damage(GameObject player)
    {
        Debug.Log(player.GetComponent<PlayerAttack>().SpeBarreSlider.gameObject);

        transform.localScale -= new Vector3(transform.localScale.x - (transform.localScale.x/10), transform.localScale.x - (transform.localScale.y / 10), transform.localScale.z - (transform.localScale.x / 10));//Anim

        hp--;

        //Debug.Log("Fish to try : " + nextFish + " next gold fish" + nextGFish);

        int xcount = Random.Range(0, 5);

        switch (xcount)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("Eating1");
                break;
            case 1:
                FindObjectOfType<AudioManager>().Play("Eating2");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play("Eating3");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Play("Eating4");
                break;
            case 4:
                FindObjectOfType<AudioManager>().Play("Eating5");
                break;
            case 5:
                FindObjectOfType<AudioManager>().Play("Eating6");
                break;
        }
        GameObject fui =  Instantiate(fishToUI, transform.position, Quaternion.identity);
        Destroy(fui, 1.4f);
        fui.GetComponent<MoveToUI>().ui_element_gameobject = player.GetComponent<PlayerAttack>().SpeBarreSlider.gameObject;

        StartCoroutine(BarSpePlus(player));
        animator.SetTrigger("SHAKEMETHAT");

        if (hp <= 0)
        {
            Destroy(gameObject);
            PointAreaManager.instance.StartNextSpawn();
        }
    }

    IEnumerator BarSpePlus(GameObject player)
    {
        yield return new WaitForSecondsRealtime(1.4f);
        if (isGolden)//le poisson est goldé
        {
            //Debug.Log("GOLDEEEEEEENNNNNNNN");
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreGoldPointArea, player.GetComponent<Player>());
            if (player.GetComponent<PlayerAttack>().CurrentSpecial < player.GetComponent<PlayerAttack>().maxSpecial)
                player.GetComponent<PlayerAttack>().AddSpeBarrePoint(ScoreManager.instance.scoreGoldPointArea);
            if (player.GetComponent<PlayerAttack>().CurrentSpecial > player.GetComponent<PlayerAttack>().maxSpecial)
                player.GetComponent<PlayerAttack>().CurrentSpecial = 5;
        }
        else//bouh le nul
        {
            ScoreManager.instance.AddScore(ScoreManager.instance.scorePointArea, player.GetComponent<Player>());
            if (player.GetComponent<PlayerAttack>().CurrentSpecial < player.GetComponent<PlayerAttack>().maxSpecial)
                player.GetComponent<PlayerAttack>().AddSpeBarrePoint(ScoreManager.instance.scorePointArea);
        }
    }
}
