using System;
using System.Collections.Generic;
using System.Linq;
using Node;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

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
        [SerializeField] protected BaseNode _node;
        [SerializeField] protected NodeConnectorType _nodeConnectorTypeRequired;
        
        public BaseNode OwnerNode
        {
            get => _node;
        }
        
        [SerializeField] protected PointerCatcher _end;
        [SerializeField] protected GameObject _solid;
        
        protected List<Connection> _connections = new List<Connection>();
        
        private void Awake()
        {
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

        protected abstract bool CheckoutMode();
    }
}
