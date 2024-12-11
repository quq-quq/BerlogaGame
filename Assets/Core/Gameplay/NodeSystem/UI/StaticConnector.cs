using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class StaticConnector : BaseConnector
    {
        [SerializeField] private List<ConnectorEnter> _enters;

        protected override void OnAwake()
        {
            foreach (var enter in _enters)
            {
                var c = new Connection(_end, _solid, this);
                c.MoveConnect(enter.transform.position);
                c.FinishConnect();
                _connections.Add(c);
            }
            
        }

        private void Start()
        {
            _enters.ForEach(i => i.MakeSealed());
        }

        protected override void OnLateUpdate()
        {
        }

        protected override bool CheckoutMode()
        {
            return true;
            
        }
    }
}
