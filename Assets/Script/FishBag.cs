using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBag : MonoBehaviour
{
    [SerializeField] public int hp = 11;
    private Animator animator;
    public Transform[] fish;
    public List<int> goldenFish;

    private int nextFish = 0;
    private int nextGFish = 0;

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
        hp = fish.Length;
    }

    public void Damage(GameObject player)
    {
        transform.localScale -= new Vector3(transform.localScale.x - (transform.localScale.x/10), transform.localScale.x - (transform.localScale.y / 10), transform.localScale.z - (transform.localScale.x / 10));//Anim

        hp--;

        Debug.Log("Fish to try : " + nextFish + " next gold fish" + nextGFish);

        if (goldenFish.Count > nextGFish && nextFish == goldenFish[nextGFish])//le poisson est goldé
        {
            Debug.Log("GOLDEEEEEEENNNNNNNN");
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreGoldPointArea, player.GetComponent<Player>());
            if (player.GetComponent<PlayerAttack>().CurrentSpecial < player.GetComponent<PlayerAttack>().maxSpecial)
                player.GetComponent<PlayerAttack>().AddSpeBarrePoint(ScoreManager.instance.scoreGoldPointArea);
            if (player.GetComponent<PlayerAttack>().CurrentSpecial > player.GetComponent<PlayerAttack>().maxSpecial)
                player.GetComponent<PlayerAttack>().CurrentSpecial = 5;
            nextGFish++;

        }
        else//bouh le nul
        {
            ScoreManager.instance.AddScore(ScoreManager.instance.scorePointArea, player.GetComponent<Player>());
            if (player.GetComponent<PlayerAttack>().CurrentSpecial < player.GetComponent<PlayerAttack>().maxSpecial)
                player.GetComponent<PlayerAttack>().AddSpeBarrePoint(ScoreManager.instance.scorePointArea);
        }
        
        Destroy(fish[nextFish].gameObject);
        nextFish++;

        animator.SetTrigger("SHAKEMETHAT");

        if (hp <= 0)
        {
            Destroy(gameObject);
            PointAreaManager.instance.StartNextSpawn();
        }
    }
}
