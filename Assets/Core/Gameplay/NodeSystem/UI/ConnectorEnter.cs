using System;
using System.Collections.Generic;
using System.Linq;
using Node;
using UnityEngine;

namespace UI
{
    public class ConnectorEnter : MonoBehaviour
    {
        
        private BaseNode _node;

        private bool _isSealed = false;
        
        public bool IsSealed => _isSealed;

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
            _connectorEnters.Where(i => i._node != null).ToList().ForEach(i => newL.Add(i));
            return newL;
        }
        
        public bool IsConnectable(BaseConnector connection)
        {
            if(_isSealed)
                return false;
            return _connections.All(item => item != connection) && !_node.IsConnected(connection.OwnerNode);
        }

        public bool TryConnect(BaseConnector connection)
        {
            var able = IsConnectable(connection);
            if(able)
                Connect(connection);
            return able;
        }
        
        public void Connect(BaseConnector connection)
        {
            if (!IsConnectable(connection)) throw new AggregateException($"Can't connect able from {connection.OwnerNode} node, to {Node}");
            _connections.Add(connection);
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

        public void SealedConnect(BaseConnector connection)
        {
            _isSealed = false;
            if (!IsConnectable(connection)) throw new AggregateException($"Can't connect able from {connection.OwnerNode} node, to {Node}");
            _connections.Add(connection);
            _isSealed = true;
        }
        

        public void Boot()
        {
            _node = GetComponentInParent<BaseNode>(true);
            _connectorEnters.Add(this);
        }

        private void OnDestroy()
        {
            _connectorEnters.Remove(this);
        }
    }
}
