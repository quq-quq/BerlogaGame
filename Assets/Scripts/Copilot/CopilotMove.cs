using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CopilotMove : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _gravity;
    [SerializeField] private Transform _followCorner;
    [SerializeField, Min(0f)] private float _distanceTranquility;
    [SerializeField, Min(0f)] private float _distanceReachPoint;
    [SerializeField] private float _timeChangePosition;
[Space]
    private Vector3 _currentFollowPoint;
    private Vector3 _direction;
    private float _currentSpeed = 0;
    private bool _reached = false;
    private bool _isGoToCorner = false;

    private void Awake()
    {
        StartCoroutine(ChangeFollowPointCoroutine());
    }

    private IEnumerator ChangeFollowPointCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeChangePosition);
            if ((transform.position - _followCorner.position).magnitude < _distanceTranquility)
            {
                ChangeFollowPoint();
            }
        }
    }
    
    private void FixedUpdate()
    {
        if(!_reached && _isGoToCorner)
        {
            _currentFollowPoint = _followCorner.position;
        }

        if ((transform.position - _followCorner.position).magnitude > _distanceTranquility )
        {
            ChangeFollowPoint();
        }
        if ((transform.position - _currentFollowPoint).magnitude <= _distanceReachPoint )
        {
            _reached = true;
        }

        if (_reached)
        {
            _currentSpeed -= _acceleration * Time.fixedDeltaTime;
        }
        else
        {
            _currentSpeed += _acceleration * Time.fixedDeltaTime;
            _direction = _currentFollowPoint - transform.position;

        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
        

        var t = (_currentFollowPoint - transform.position).magnitude / _distanceTranquility;
        transform.position += _direction * (_currentSpeed * Time.fixedDeltaTime) +
                              (_reached ? Vector3.zero : (Mathf.Lerp(0f, _gravity * Time.fixedDeltaTime, t) * Vector3.down));

    }

    private void ChangeFollowPoint()
    {
        
        _reached = false;
        if ((transform.position - _followCorner.position).magnitude > _distanceTranquility)
        {
            _currentFollowPoint = _followCorner.position;
            _isGoToCorner = true;
            return;
        }

        _isGoToCorner = false;
        
        // var x = Random.Range(-_distanceTranquility, _distanceTranquility);
        // var y = Random.Range(-_distanceTranquility, _distanceTranquility);
        // _lastRandomDelta = new Vector3(x, y, 0);
        // _currentFollowPoint = _followCorner.position + _lastRandomDelta;
        _currentFollowPoint = _points[Random.Range(0, _points.Count)].position;
    }
}
