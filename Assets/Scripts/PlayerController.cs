using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class PlayerController : AnimatorController
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private float _startSpeed;
    private bool _isJump;

    void Start()
    {
        BaseStart();
        _startSpeed = _speed;
    }

    void Update()
    {
        BaseUpdate();

        if (!_isJump)
        {
            transform.Translate(new Vector2(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * _speed);
        }

        if(Input.GetAxis("Horizontal") == 0)
        {
            _speed = 0;
        }
        else if(_speed < _startSpeed)
        {
            _speed += _startSpeed*_startSpeed*Time.deltaTime*0.5f ;
        }

        if (Input.GetKey(KeyCode.Space) && !_isJump)
        {
            //_rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); alternative way
            rb.velocity = new Vector2(Input.GetAxis("Horizontal")*_speed, _jumpForce);

            _isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isJump = false;
        }
    }
}
