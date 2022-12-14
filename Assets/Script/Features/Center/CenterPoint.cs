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
        basePosition = globalText.transform.localPosition;
        currentScore = 0;
        //Debug.Log(globalText.GetComponent<TextMeshProUGUI>());
        globalText.GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
        playerInMiddle = _player;
    }

    public void PopPoint()
    {
        Br();
        globalText.transform.DOPunchScale(globalText.transform.localScale * 2, .5f, 2, 0);
        currentScore += ScoreManager.instance.scoreMiddle;
        globalText.GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
    }

    private void Br()
    {
        int _strength = currentScore / ScoreManager.instance.scoreMiddle;
        _strength = _strength >= 10 ? _strength = 10 : _strength = currentScore / ScoreManager.instance.scoreMiddle;


        globalText.transform.DOShakePosition(ScoreManager.instance.middelPointsCooldown, new Vector3(.05f,.05f,.05f), _strength, 90, false, false);
    }

    public IEnumerator PopPointText()
    {
        yield return new WaitForSecondsRealtime(0.5f);        

    }

    public void PointToUIF()
    {
        Vector3 screenPoint = ScoreManager.instance.scorePlayerText[playerInMiddle.playerID].transform.position + new Vector3(0, 0, 5);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

        globalText.transform.DOJump(worldPos, 1, 1, .5f).SetEase(Ease.InOutBack).onComplete += () =>
        {
            ScoreManager.instance.AddScore(currentScore, playerInMiddle);
            globalText.transform.localPosition = basePosition;
            globalText.SetActive(false);
        };
    }
}
