using System.Collections.Generic;
using System.Linq;
using Node_System.Scripts.Node;
using UI;
using UnityEngine;

namespace Node
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseNode : MonoBehaviour
    {
        [SerializeField] private string _nodeName;
        public string NodeName => _nodeName;
        
        private BaseConnector _connector;
        private RectTransform _rectTransform;
        private ConnectorEnter _connectorEnter; 
        
        public RectTransform RectTransform
        {
            get
            {
                if(_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public BaseConnector Connector
        {
            get
            {
                if (_connector == null && this != null)
                {
                    _connector = GetComponentInChildren<BaseConnector>(true);
                }

                return _connector;
            }
        }
        
        public ConnectorEnter Enter
        {
            get
            {
                if (_connectorEnter == null)
                {
                    _connectorEnter = GetComponentInChildren<ConnectorEnter>(true);
                }

                return _connectorEnter;
            }
        }

        public abstract void Do(ObjectForNode go);
        public abstract void Boot();

        public bool IsConnected(BaseNode node)
        {
            var connectedNodes = Connector.GetConnectedNodes();
            if(connectedNodes == null || connectedNodes.Count == 0)
                return false;

            return  connectedNodes.Any(i => i == node || i.IsConnected(node));
        }
        
        public List<T> GetConnectedNodeOfType<T>() where T : BaseNode
        {
            var result = new List<T>();
            var enter = Enter;
            if (enter == null || enter.Connections.Count == 0)
                return result;
            
            var connections = enter.Connections.ToList();
            for (var i = connections.Count-1; i >= 0; i--)
            {
                var connection = connections[i];
                if (connection.OwnerNode is T node)
                {
                    result.Add(node);
                    connections.Remove(connection);
                }
            }

            foreach (var connection in connections)
            {
                result.AddRange(connection.OwnerNode.GetConnectedNodeOfType<T>());
            }

            return result;
        }
    }
}
