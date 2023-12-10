using System;
using System.Collections.Generic;
using DefaultNamespace;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;

namespace NodeObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class Quadrocopter : ObjectForNode
    {
        [SerializeField] private TriggerZone _zone;
        [SerializeField] private SpriteRenderer _spriteRange;
        [SerializeField] private Transform _followObject;
        [SerializeField] private bool _isFollow = true;
        [SerializeField, Min(0.001f)] private float _lerp;

        [SerializeField] private Color _zoneOnColor;
        [SerializeField] private Color _zoneOffColor;


        private List<ISleeper> _sleepers = new List<ISleeper>();
        private bool _isStartSleep = false;

        private void Awake()
        {
            _zone.TriggerEnter += OnZoneEnter;
            _zone.TriggerExit += OnZoneExit;
        }

        public void StartSleep()
        {
            _isStartSleep = true;
            foreach (var i in _sleepers)
            {
                i.Sleep(this);
            }
            _spriteRange.color = _zoneOnColor;
        }
        
        public void StopSleep()
        {
            _isStartSleep = false;
            foreach (var i in _sleepers)
            {
                i.WakeUp(this);
            }
            _spriteRange.color = _zoneOffColor;
        }

        private void OnZoneEnter(Collider2D other)
        {
            if(other.TryGetComponent(out ISleeper sleeper))
            {
                if(_isStartSleep)
                    sleeper.Sleep(this);
                _sleepers.Add(sleeper);
            }
        }
        private void OnZoneExit(Collider2D other)
        {
            if(other.TryGetComponent(out ISleeper sleeper))
            {
                if(_sleepers.Remove(sleeper))
                {
                    sleeper.WakeUp(this);
                }
            }
        }

        public void StopFollow()
        {
            _isFollow = false;
        }
        
        public void StartFollow()
        {
            _isFollow = true;
        }

        private void FixedUpdate()
        {
            if(!_isFollow)
                return;

            transform.position = Vector3.Lerp(transform.position, _followObject.position, _lerp);
        }
    }
}
