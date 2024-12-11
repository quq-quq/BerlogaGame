using System;
using System.Linq;
using Node_System.Scripts.Node;
using UI;
using UnityEngine;

namespace Node
{
    public abstract class BaseNode : MonoBehaviour
    {
        private BaseConnector _connector;

        public BaseConnector Connector
        {
            get
            {
                if (_connector == null)
                {
                    _connector = GetComponentInChildren<BaseConnector>();
                }

                return _connector;

            }
        }

        public abstract void Do(ObjectForNode go);

        public bool IsConnected(BaseNode node)
        {
            var connectedNodes = Connector.GetConnectedNodes();
            if(connectedNodes.Count == 0)
                return false;

            return  connectedNodes.Any(i => i == node || i.IsConnected(node));
        }
    }
}
