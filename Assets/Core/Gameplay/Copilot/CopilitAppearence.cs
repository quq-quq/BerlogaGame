using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CopilitAppearence : MonoBehaviour
{
    [SerializeField] private Transform _copilotTransform;
    [Space(20)]
    [SerializeField] private int _notInTheFrameTime;
    [SerializeField] private int _inTheFrameTime;
    [Space(10)]
    [SerializeField] private int _distance;

    private void Awake()
    {
        StartCoroutine(NotInTheFrame());   
    }

    private IEnumerator NotInTheFrame()
    {
        _copilotTransform.Translate(Vector3.up * _distance);
        yield return new WaitForSeconds(_notInTheFrameTime);
        StartCoroutine(InTheFrame());
    }

    private IEnumerator InTheFrame()
    {
        _copilotTransform.Translate(Vector3.down * _distance);
        yield return new WaitForSeconds(_inTheFrameTime);
        StartCoroutine(NotInTheFrame());
    }

}
