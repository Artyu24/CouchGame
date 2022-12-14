using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class FishBag : MonoBehaviour
{
    [SerializeField] public int hp = 1;
    private Animator animator;

    private GameObject fishToUI;
    public bool isGolden;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        transform.GetChild(0).localScale = new Vector3(0.05f, 0.05f, 0.05f);
        transform.GetChild(0).DOScale(new Vector3(2.5f, 5, 5), 1f).SetEase(Ease.OutBack);

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

        FindObjectOfType<AudioManager>().PlayRandom(SoundState.EatingSound);
        
        GameObject fui =  Instantiate(fishToUI, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, PlayerManager.instance.canvasUI.transform);
        fui.GetComponent<MoveToUI>().ui_element_gameobject = player.GetComponent<PlayerAttack>().SpeBarreSlider.gameObject;

        ObjectManager.Instance.CallBarSpePlus(player, isGolden);
        animator.SetTrigger("SHAKEMETHAT");

        if (hp <= 0)
        {
            gameObject.layer = 12;

            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);

            ObjectManager.Instance.StartNextSpawn();
            
            Vector3 scale = transform.GetChild(1).localScale;
            transform.GetChild(1).DOScale(scale, 1f).OnComplete(() => transform.GetChild(1).DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject)));
        }
    }
}
