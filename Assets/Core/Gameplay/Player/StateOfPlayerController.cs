using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class StateOfPlayerController : MonoBehaviour
{
    private bool _isEnabled = true;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void SetStateOfComponent()
    {
        if(_isEnabled)
        {
            _playerController.enabled = false;
            _isEnabled = false;
        }
        else
        {
            _playerController.enabled = true;
            _isEnabled = true;
        }
    } 
}
