using System.Collections.Generic;
using Node;
using UnityEngine;

namespace UI
{
    public class StaticConnector : BaseConnector
    {
        [SerializeField] private List<ConnectorEnter> _enters;
        private bool _isSealed = false;

        protected override void OnAwake()
        {
            foreach (var enter in _enters)
            {
                if (enter == null)
                    continue;
                var c = new Connection(_end, _solid, this);
                c.SealedConnect(enter);
                c.MoveConnect(enter.transform.position);
                _connections.Add(c);
            }
            _isSealed = true;
        }

        protected override void OnLateUpdate()
        {
        }

        public override bool CheckoutMode(BaseNode node)
        {
            return !_isSealed;
            
        }
    }
}
