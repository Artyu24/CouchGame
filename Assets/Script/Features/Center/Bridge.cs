using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public void ActivateBridge()
    {
        transform.DOScaleZ(0.5f, 5f);
    }

    public void DesactivateBridge()
    {
        transform.DOScaleZ(0.1f, 5f);
    }
}
