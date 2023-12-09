using System;
using System.Collections;
using DefaultNamespace;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;

namespace NodeObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class CameraDetector : TriggerForNode, ISleeper
    {
        [SerializeField] private SpriteRenderer _zone;
        private event Action _triger;
        [SerializeField]private bool _isSleep = false;
        private ISleeper _sleeper;
        private Color _colorBackup;

        private void Awake()
        {
            _colorBackup = _zone.color;
            _sleeper = new SimpleSleeper(() =>
            {
                _isSleep = !_isSleep;
                SetZoneColor();
            }, this);
            SetZoneColor();
        }

        private void SetZoneColor()
        {
            var colorTemp = _colorBackup;
            if(_isSleep)
                colorTemp.a = 0f;
            _zone.color = colorTemp;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_isSleep)
                return;
            if (other.TryGetComponent(out Detectable i))
                Trigger();
        } 

        public override void SubscribeTrigger(Action action)
        {
            _triger += action;
        }

        private void Trigger()
        {
            _triger?.Invoke();
        }

        public void Sleep(float t, object caller)
        {
            _sleeper.Sleep(t, caller);
        }

        public void Sleep(object caller)
        {
            _sleeper.Sleep(caller);
        }

        public void WakeUp(object caller)
        {
            _sleeper.WakeUp(caller);
        }
    }
}
