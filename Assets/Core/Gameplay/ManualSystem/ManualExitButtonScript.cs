using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualExitButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject _manualButton;

    private void OnEnable()
    {
        _manualButton.SetActive(false);
    }

    private void OnDisable()
    {
        _manualButton.SetActive(true);
    }
}
