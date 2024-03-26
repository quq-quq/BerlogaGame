using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;

namespace NodeObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class CameraDetector : MonoBehaviour, ISleeper
    {
        [SerializeField] private SpriteRenderer _zone;
        [SerializeField]private bool _isSleep = false;
        private ISleeper _sleeper;
        private Color _colorBackup;

        [SerializeField] private SimpleTrigger _enterTrigger;
        [SerializeField] private SimpleTrigger _exitTrigger;

        public event Action EnterEvent;
        public event Action ExitEvent;

        [Space(40)]
        [SerializeField] private AudioClip _sleepAudio;
        [SerializeField] private float _volume;

        private List<Detectable> _detectables = new List<Detectable>();

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

        private void EnterTriggerInvoke()
        {
            if(_isSleep)
                return;
            _enterTrigger.Invoke();
        }

        public void FixedUpdate()
        {
            if (_detectables.All(x => !x.IsDetectable))
                ExitTriggerInvoke();
                
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Detectable d) && _detectables.All(i => i != d))
            {
                d.StateChangedEvent += OnDetectableChangeState;
                _detectables.Add(d);
                if(d.IsDetectable)
                {
                    EnterTriggerInvoke();
                    if(d.TryGetComponent(out PlayerController p ))
                        EnterEvent?.Invoke();
                }
                    
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.TryGetComponent(out Detectable d) && _detectables.Any(i => i == d))
            {
                d.StateChangedEvent -= OnDetectableChangeState;
                if(_detectables.Remove(d) && d.IsDetectable && _detectables.Count == 0)
                {
                    ExitTriggerInvoke();
                    if(d.TryGetComponent(out PlayerController p ))
                        ExitEvent?.Invoke();
                }
            }
        }

        private void ExitTriggerInvoke()
        {
            if(_isSleep)
                return;
            _exitTrigger.Invoke();
        }

        private void OnDetectableChangeState(bool newState)
        {
            if(newState)
                EnterTriggerInvoke();
            else
                ExitTriggerInvoke();
            
        }

        public void Sleep(float t, object caller)
        {
            SoundController.sounder.SetSound(_sleepAudio, false, gameObject.name, _volume);

            if (_detectables.Count != 0)
            {
                ExitTriggerInvoke();
            }
            _sleeper.Sleep(t, caller);
        }

        public void Sleep(object caller)
        {
            if(_detectables.Count != 0)
            {
                ExitTriggerInvoke();
            }
            _sleeper.Sleep(caller);

            SoundController.sounder.SetSound(_sleepAudio, false, gameObject.name, _volume);
        }

        public void WakeUp(object caller)
        {
            _sleeper.WakeUp(caller);
            if(_detectables.Count != 0)
            {
                EnterTriggerInvoke();
            }
        }
    }
}
