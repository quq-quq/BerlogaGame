using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;
using UnityEngine.Serialization;

namespace NodeObjects
{
    [RequireComponent(typeof(Detectable), typeof(Rigidbody2D))]
public class GalagramRobot : ObjectForNode, IHorizontalMover
{
    [SerializeField] private bool _isActive;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _skinOff;
    [SerializeField] private GameObject _skinOn;

    private Detectable _detectable;
    private Rigidbody2D _rigidbody;

    private Queue<float> _moving = new();

    private bool _isNeedUpdate;
    private float _currentMove = 0;
    private bool _firstMove = false;

    private void Start()
    {
        _detectable = GetComponent<Detectable>();
        _rigidbody = GetComponent<Rigidbody2D>();
        UpdateState();
    }

    public void UpdateState()
    {
        _isNeedUpdate = false;
        //_skinOff.SetActive(!_isActive);
        _skinOn.SetActive(_isActive);
        _detectable.IsDetectable = _isActive;
    }

    public void MoveHorizontal(float f)
    {
        if(!_isNeedUpdate)
            _firstMove = true;
        
        _moving.Enqueue(f);
    }

    public void ActivateGalagram()
    {
        _isActive = true;

        if(_firstMove)
            _isNeedUpdate = true;
        else
            UpdateState();
    }
    
    public void DeactivateGalagram()
    {
        _isActive = false;

        if(_firstMove)
            _isNeedUpdate = true;
        else
            UpdateState();
    }

    private void FixedUpdate()
    {
        if(_currentMove == 0)
        {
            if(_isNeedUpdate && ! _firstMove)
            {
                UpdateState();
            }
            if(_moving.Count != 0 && (!_isActive || _firstMove))
            {
                _currentMove = _moving.Dequeue();
                StartCoroutine(Move());
            }
        }
    }

    IEnumerator Move()
    {
        var t = Mathf.Abs(_currentMove / _speed);
        var tSpeed = _currentMove >= 0 ? _speed : -_speed;
        while (t > 0)
        {
            _rigidbody.MovePosition(transform.position + Vector3.right * (tSpeed * Time.fixedDeltaTime));
            t -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        _currentMove = 0;
        _firstMove = false;
    }
}
}
