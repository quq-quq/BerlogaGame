using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerOLD : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;    
    public bool isJump;
    private float _startSpeed;
    private Rigidbody2D rb;
    private Vector2 currentPosition, previousPosition, direction;
    public event Action<Vector2> MoveEvent;

    [Space(30)]
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _feetPos;

    void Start()
    {
        previousPosition = transform.position;
        _startSpeed = _speed;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        previousPosition = currentPosition;
        currentPosition = transform.position;
        direction = currentPosition - previousPosition;

        isJump = !Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _whatIsGround);

        var horizontalInput = Input.GetAxis("Horizontal");

        MoveEvent?.Invoke(direction);

        if (!isJump)
        {
            transform.Translate(new Vector2(horizontalInput, 0) * Time.deltaTime * _speed);

            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(horizontalInput * _speed, _jumpForce);
            }
        }

        if (horizontalInput == 0)
        {
            _speed = 0;
        }
        else if (_speed < _startSpeed)
        {
            _speed += _startSpeed * _startSpeed * Time.deltaTime * 0.5f;
        }

        if (direction.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_feetPos.position, _checkRadius);
    }
}
