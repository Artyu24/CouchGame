using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private void ActivateBridge()
    {
        transform.DOScaleZ(1, 5f);
    }

    private void DesactivateBridge()
    {
        transform.DOScaleZ(0.1f, 5f);
    }
}
