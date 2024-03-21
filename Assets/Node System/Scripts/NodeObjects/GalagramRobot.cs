using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;

namespace NodeObjects
{

    public enum GalagramRobotOperationType
    {
        Move,
        ChangeActive
    }

    public class GalagramOperation
    {
        public GalagramRobotOperationType OperationType { get; set; }
        public float Distance { get; set; }
        public bool Active { get; set; }

        public GalagramOperation(float f)
        {
            OperationType = GalagramRobotOperationType.Move;
            Distance = f;
        }
        
        public GalagramOperation(bool b)
        {
            OperationType = GalagramRobotOperationType.ChangeActive;
            Active = b;
        }
    }
    
    [RequireComponent(typeof(Detectable), typeof(Rigidbody2D))]
    public class GalagramRobot : ObjectForNode, IHorizontalMover
    {
        [SerializeField] private bool _isActive;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _skinOff;
        [SerializeField] private GameObject _skinOn;
        public Action ActivateEvent;
        public Action DisactivateEvent;
        private Detectable _detectable;
        private Rigidbody2D _rigidbody;

        private Queue<GalagramOperation> _opertions = new();
        
        private bool _canDoNextOperation = true;

        private void Start()
        {
            _detectable = GetComponent<Detectable>();
            _rigidbody = GetComponent<Rigidbody2D>();
            UpdateState(_isActive);
        }

        public void UpdateState(bool isActive)
        {
            if (isActive)
            {
                ActivateEvent?.Invoke();
            }
            else
            {
                DisactivateEvent?.Invoke();
            }
            _isActive = isActive;
            //_skinOff.SetActive(!_isActive); //i removed that to get active an animation of bot under the galagram
            _skinOn.SetActive(_isActive);
            _detectable.IsDetectable = _isActive;
            _canDoNextOperation = true;
        }

        public void MoveHorizontal(float f)
        {
            _opertions.Enqueue(new GalagramOperation(f));
        }

        public void ActivateGalagram()
        {
            _opertions.Enqueue(new GalagramOperation(true));
        }
        
        public void DeactivateGalagram()
        {
            _opertions.Enqueue(new GalagramOperation(false));
        }

        private void FixedUpdate()
        {
            if (_opertions.Count == 0 || !_canDoNextOperation) return;
            _canDoNextOperation = false;
            var t = _opertions.Dequeue();
            switch (t.OperationType)
            {
                case GalagramRobotOperationType.Move:
                    if (_isActive)
                    {
                        _canDoNextOperation = true;
                    }
                    else
                    {
                        StartCoroutine(Move(t.Distance));
                    }
                    break;
                case GalagramRobotOperationType.ChangeActive:
                    UpdateState(t.Active);
                    break;
            }
        }

        IEnumerator Move(float f)
        {
            var t = Mathf.Abs(f / _speed);
            var tSpeed = f >= 0 ? _speed : -_speed;
            while (t > 0)
            {
                _rigidbody.MovePosition(transform.position + Vector3.right * (tSpeed * Time.fixedDeltaTime));
                t -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _canDoNextOperation = true;
        }
    }
}
