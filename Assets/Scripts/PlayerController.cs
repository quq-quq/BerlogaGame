using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [HideInInspector] public bool isJump;
    private float _startSpeed;
    private Rigidbody2D rb;
    private Vector2 currentPosition, previousPosition, direction;
    public event Action<Vector2> MoveEvent;


    void Start()
    {
        previousPosition = transform.position;
        _startSpeed = _speed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentPosition = transform.position;
        direction = currentPosition - previousPosition;
        previousPosition = currentPosition;

        var horizontalInput = Input.GetAxis("Horizontal");

        if (!isJump)
        {
            transform.Translate(new Vector2(horizontalInput, 0) * Time.deltaTime * _speed);

            if (Input.GetKey(KeyCode.Space))
            {
                //_rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); alternative way
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed, _jumpForce);

                isJump = true;
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
            transform.localScale = new Vector2(1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        MoveEvent?.Invoke(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}
