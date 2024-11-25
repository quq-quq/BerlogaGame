using System;
using System.Collections.Generic;
using System.Linq;
using Node;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ConnectorEnter : MonoBehaviour
    {
        
        private BaseNode _node;

        private bool _isSealed = false;
        private List<BaseConnector> _connections = new List<BaseConnector>();

        public BaseNode Node
        {
            get => _node;
            private set => _node = value;
        }

        private static List<ConnectorEnter> _connectorEnters = new List<ConnectorEnter>();
        
        public static List<ConnectorEnter> GetConnectorEnters()
        {
            var newL = new List<ConnectorEnter>(_connectorEnters.Count);
            _connectorEnters.ForEach(i => newL.Add(i));
            return newL;
        }

        public bool Connect(BaseConnector connection)
        {
            if(_isSealed)
                return false;
            
            if(_connections.All(item => item != connection) && !_node.IsConnected(connection.OwnerNode))
            {
                _connections.Add(connection);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Disconnect(BaseConnector connection)
        {
            if(_connections.All(item => item != connection))
            {
                Debug.LogError("Connection not found");
            }
            _connections.Remove(connection);
        }

        public void MakeSealed()
        {
            _isSealed = true;
        }

        private void Awake()
        {
            _node = GetComponentInParent<BaseNode>();
            _connectorEnters.Add(this);
        }

        private void OnDestroy()
        {
            _connectorEnters.Remove(this);
        }
    }
}
