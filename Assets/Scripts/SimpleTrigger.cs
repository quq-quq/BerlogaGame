using System;
using Node_System.Scripts.Node;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class SimpleTrigger : TriggerForNode
    {
        private Action _trigger;
        public override void SubscribeTrigger(Action action)
        {
            _trigger += action;
        }

        public void Invoke()
        {
            _trigger?.Invoke();
        }
    }
}
