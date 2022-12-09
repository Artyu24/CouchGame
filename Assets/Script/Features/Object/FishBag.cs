using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishBag : MonoBehaviour
{
    [SerializeField] public int hp = 1;
    private Animator animator;
    public Transform[] fish;

    [SerializeField] private BoxCollider boxOne, boxTwo;

    private GameObject fishToUI;
    public bool isGolden;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
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
        hp--;
        int xcount = Random.Range(0, 5);

        //Debug.LogError("JM ICI LE SON FRERO T ES BO");
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.EatingSound);
        
        GameObject fui =  Instantiate(fishToUI, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, PlayerManager.instance.canvasUI.transform);
        fui.GetComponent<MoveToUI>().ui_element_gameobject = player.GetComponent<PlayerAttack>().SpeBarreSlider.gameObject;

        ObjectManager.Instance.CallBarSpePlus(player, isGolden);
        animator.SetTrigger("SHAKEMETHAT");

        if (hp <= 0)
        {
            boxOne.enabled = false;
            boxTwo.enabled = false;

            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.layer = 0;
            Destroy(gameObject, 1.0f);
            ObjectManager.Instance.StartNextSpawn();
        }
    }

    
}
