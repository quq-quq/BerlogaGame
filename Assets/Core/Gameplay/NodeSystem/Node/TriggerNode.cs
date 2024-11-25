using System;
using Node;
using TMPro;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class TriggerNode : BaseNode
    {
        [SerializeField] private TriggerForNode _trigger;

        private void Awake()  
        {
            if (_trigger != null)
                _trigger.SubscribeTrigger(() => Do(null));
        }

        public override void Do(ObjectForNode go)
        {
            Connector.GetConnectedNodes().ForEach(i => i.Do(go));
        }
    }
}
