using System;
using System.Linq;
using UI;
using UnityEngine;

namespace Node
{
    public abstract class BaseNode : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private BaseConnector _connector;

        public BaseConnector Connector => _connector;

        public string NameNode
        {
            get => _name;
            protected set => _name = value;
        }

        public abstract void Do(GameObject go);

        public bool IsConnected(BaseNode node)
        {
            var connectedNodes = _connector.GetConnectedNodes();
            if(connectedNodes.Count == 0)
                return false;

            return  connectedNodes.Any(i => i == node || i.IsConnected(node));
        }
    }
}
