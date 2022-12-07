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
        //Debug.Log(player.GetComponent<PlayerAttack>().SpeBarreSlider.gameObject);

        //transform.localScale -= new Vector3(transform.localScale.x - (transform.localScale.x/10), transform.localScale.x - (transform.localScale.y / 10), transform.localScale.z - (transform.localScale.x / 10));//Anim

        hp--;

        //Debug.Log("Fish to try : " + nextFish + " next gold fish" + nextGFish);

        int xcount = Random.Range(0, 5);

        //Debug.LogError("JM ICI LE SON FRERO T ES BO");
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.EatingSound);
        
        GameObject fui =  Instantiate(fishToUI, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameManager.instance.Ui);
        fui.GetComponent<MoveToUI>().ui_element_gameobject = player.GetComponent<PlayerAttack>().SpeBarreSlider.gameObject;

        ObjectManager.Instance.CallBarSpePlus(player, isGolden);
        animator.SetTrigger("SHAKEMETHAT");

        if (hp <= 0)
        {
            ObjectManager.Instance.StartNextSpawn();
            Destroy(gameObject);
        }
    }

    
}
