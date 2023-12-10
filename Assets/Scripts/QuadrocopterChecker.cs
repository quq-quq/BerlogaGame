using System;
using DefaultNamespace;
using NodeObjects;
using UnityEngine;

public class QuadrocopterChecker : MonoBehaviour
{
    [SerializeField] private SimpleTrigger _triggerEnter;
    [SerializeField] private SimpleTrigger _triggerExit;

    [SerializeField] private GameObject _skinOn;
    [SerializeField] private GameObject _skinOff;

    private void Awake()
    {
        _skinOn.SetActive(false);
        _skinOff.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _skinOn.SetActive(true);
        _skinOff.SetActive(false);
        if(other.TryGetComponent(out Quadrocopter q))
            _triggerEnter.Invoke();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _skinOn.SetActive(false);
        _skinOff.SetActive(true);
        if(other.TryGetComponent(out Quadrocopter q))
            _triggerExit.Invoke();
    }
}
