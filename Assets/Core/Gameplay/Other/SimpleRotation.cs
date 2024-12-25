using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    [SerializeField, Min(0)] float _duration;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 180), _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}
