using System;
using System.Collections.Generic;
using Node;
using UnityEngine;

namespace UI
{
    public enum NodeConnectorType
    {
        Object,
        Action,
        ObjectAndAction
    }

    [RequireComponent(typeof(PointerCatcher))]
    public abstract class BaseConnector : MonoBehaviour
    {
        [SerializeField] protected PointerCatcher _end;
        [SerializeField] protected GameObject _solid;
        
        protected BaseNode _node;
        
        public event Action<Connection> OnConnect;

        protected void InvokeConnection(Connection connection)
        {
            OnConnect?.Invoke(connection);
        }
        
        public BaseNode OwnerNode
        {
            get => _node;
        }
        
        protected List<Connection> _connections = new List<Connection>();
        
        public void Boot()
        {
            _node = GetComponentInParent<BaseNode>(true);
            _end.gameObject.SetActive(false);
            _solid.gameObject.SetActive(false);
            OnAwake();
        }

        protected abstract void OnAwake();

        private void LateUpdate()
        {
            _connections.ForEach(i => i.UpdatePosition());
            OnLateUpdate();
        }

        protected abstract void OnLateUpdate();

        public List<BaseNode> GetConnectedNodes()
        {
            var r = new List<BaseNode>();
            _connections.ForEach(i => r.Add(i.ConnectedBaseNode));
            return r;
        }

        public abstract bool CheckoutMode(BaseNode node);
    }
}
