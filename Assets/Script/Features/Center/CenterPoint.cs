using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenterPoint : MonoBehaviour
{
    public static CenterPoint Instance;

    private GameObject globalText;

    public Player playerInMiddle;

    private int currentScore = 0;
    [SerializeField] Vector3 basePosition;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        globalText = transform.GetChild(0).gameObject;
    }
    
    public void SetUp(Player _player)
    {
        globalText.SetActive(true);
        basePosition = globalText.transform.position;
        currentScore = 0;
        //Debug.Log(globalText.GetComponent<TextMeshProUGUI>());
        globalText.GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
        playerInMiddle = _player;
    }

    public void Test()
    {
        StartCoroutine(PopPointText());
    }

    public IEnumerator PopPointText()
    {
        //GameObject t = Instantiate(pointText, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        //pointText.GetComponent<TextMeshProUGUI>().text = ScoreManager.instance.scoreMiddle.ToString();

        //t.transform.DOMove(globalText.transform.position, 0.5f).SetEase(Ease.OutElastic);
        yield return new WaitForSecondsRealtime(0.5f);

        //Destroy(t);
        globalText.transform.DOPunchScale(globalText.transform.localScale * 2, .5f, 2, 0);
        currentScore += ScoreManager.instance.scoreMiddle;
        globalText.GetComponent<TextMeshProUGUI>().text = currentScore.ToString();

    }

    public void PointToUIF()
    {
        StartCoroutine(PointToUI());
    }

    public IEnumerator PointToUI()
    {
        Vector3 screenPoint = ScoreManager.instance.scorePlayerText[playerInMiddle.playerID].transform.position + new Vector3(0, 0, 5);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

        globalText.transform.DOJump(worldPos, 1, 1, .5f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(.45f);

        ScoreManager.instance.AddScore(currentScore, playerInMiddle);
        globalText.transform.position = basePosition;
        globalText.SetActive(false);

    }
}
