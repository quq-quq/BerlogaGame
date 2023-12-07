using System;
using Node_System.Scripts.Node;

namespace UI
{
    public class StartTrigger : TriggerForNode
    {
        private event Action _triger; 

        public override void SubscribeTrigger(Action action)
        {
            _triger += action;
        }

        public void Trigger()
        {
            _triger?.Invoke();
        }
    }
}
