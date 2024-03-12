using System;
using Node_System.Scripts.Node;
using UnityEngine;

namespace UI
{
    public class StartTrigger : TriggerForNode
    {
        [SerializeField] private AudioClip _calculation;
        [SerializeField] private float _volume;

        private event Action _triger; 

        public override void SubscribeTrigger(Action action)
        {
            _triger += action;
        }

        public void Trigger()
        {
            SoundController.sounder.SetSound(_calculation, false, gameObject.name, _volume);
            _triger?.Invoke();
        }
    }
}
