using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToUI : MonoBehaviour
{
    public GameObject ui_element_gameobject;
    private int currentMoveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        test();
    }

    void test()
    {
        Vector3 screenPoint = ui_element_gameobject.transform.position + new Vector3(0, 0, 5);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);

        transform.DOJump(worldPos, 1, 1, 2).SetEase(Ease.InOutBack);
    }

}
