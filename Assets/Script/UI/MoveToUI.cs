using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToUI : MonoBehaviour
{
    public GameObject ui_element_gameobject;

    // Start is called before the first frame update
    void Start()
    {
        MoveToUi();
    }

    void MoveToUi()
    {
        Vector3 screenPoint = ui_element_gameobject.GetComponent<RectTransform>().position + new Vector3(0, 30);

        Tween a = transform.DOJump(screenPoint, 1, 1, 1.4f).SetEase(Ease.InOutCirc);
        Tween b = GetComponent<RectTransform>().DOScale(new Vector3(0.5f, 0.5f), 1.4f);
        Sequence seq = DOTween.Sequence();
        seq.Append(a).Join(b).OnComplete(() => Destroy(gameObject));


        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
    }

}
