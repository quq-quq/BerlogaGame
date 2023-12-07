using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private Animator _anim;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
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

        if (direction.x != 0 && direction.y==0)
        {
           _anim.SetBool("isRunning", true);
        }
        else
        {
            _anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.Space) && _playerController.isJump)
        {
            _playerController.isJump = true;
            _anim.SetTrigger("TakeOf");


        }
        if(!_playerController.isJump)
        {
            _anim.SetBool("isJumping", false);
        }
        else
        {
            _anim.SetBool("isJumping", true);
        }
    }
}
