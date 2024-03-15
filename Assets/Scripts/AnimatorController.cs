using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private PlayerControllerOLD _playerController;
    private Animator _anim;
    
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerControllerOLD>();
    }

    private void OnEnable()
    {
        _playerController.MoveEvent += MovementDetecting;
    }
    private void OnDisable()
    {
        _playerController.MoveEvent -= MovementDetecting;
    }

    private void MovementDetecting(Vector2 direction)
    {

        if (direction.x != 0)
        {
           _anim.SetBool("isRunning", true);
        }
        else
        {
            _anim.SetBool("isRunning", false);
        }


        if(!_playerController.isJump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _anim.SetTrigger("TakeOf");
            }
            _anim.SetBool("isJumping", false);
        }
        else
        {
            _anim.SetBool("isJumping", true);
        }
    }
}
