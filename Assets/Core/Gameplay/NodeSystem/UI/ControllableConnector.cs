using System;
using System.Linq;
using Node;
using Node_System.Scripts.Node;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ControllableConnector : BaseConnector
    {
        [SerializeField] protected NodeConnectorType _nodeConnectorTypeRequired;

        [SerializeField] private PointerCatcher _pointerCatcher;
        protected bool _isClicked = false;
        protected bool _isMoveConnection = false;
        private Connection _currentConnection = null;
        

        protected override void OnAwake()
        {
        }

        protected override void OnLateUpdate()
        {
            if (_isMoveConnection)
            {
                MoveConnection();
                return;
            }
            
            if(!_isClicked) return;
            ManageNewConnection();
        }

        public override bool CheckoutMode(BaseNode node)
        {
            if(node == null) 
                return false;
            
            var isAction = node is ActionNode;
            var isObject = node is ObjectNode;

            if(isAction)
            {
                if(_nodeConnectorTypeRequired is NodeConnectorType.Action or NodeConnectorType.ObjectAndAction)
                {
                    return true;
                }
            }
            if(isObject)
            {
                if(_nodeConnectorTypeRequired is NodeConnectorType.Object or NodeConnectorType.ObjectAndAction)
                {
                    return true;
                }     
            }
            return false;

        }

        public void ClearConnections()
        {
            foreach (var connection in _connections)
            {
                connection.Die();
                connection.ClickedDownEvent -= OnClickDownConnection;
                connection.ClickedUpEvent -= OnClickUpConnection;
            }
            _connections.Clear();
        }
        

        private void OnEnable()
        {
            _pointerCatcher.PointerDownEvent += OnPointerDown;
            _pointerCatcher.PointerUpEvent += OnPointerUp;
        }
        
        private void OnDisable()
        {
            _pointerCatcher.PointerDownEvent -= OnPointerDown;
            _pointerCatcher.PointerUpEvent -= OnPointerUp;
        }
        
        private void OnPointerDown(PointerEventData pointerEventData)
        {
            _isClicked = true;
        }
        
        private void OnPointerUp(PointerEventData pointerEventData)
        {
            _isClicked = false;
            OnClickUpConnection();
        }

        private void SubscribeConnection(Connection connect)
        {
            if(_connections.Any(i => i == connect))
                return;
            
            _connections.Add(connect);
            InvokeConnection(connect);
            connect.ClickedDownEvent += OnClickDownConnection;
            connect.ClickedUpEvent += OnClickUpConnection;
        }
        
        private void UnsubscribeConnection(Connection connect)
        {
            _connections.Remove(connect);
            connect.ClickedDownEvent -= OnClickDownConnection;
            connect.ClickedUpEvent -= OnClickUpConnection;
        }

        private void OnClickDownConnection(Connection connect)
        {
            _isMoveConnection = true;
            _currentConnection = connect;
        }
        
        private void OnClickUpConnection(Connection connect = null)
        {
            if(connect != _currentConnection && connect != null) return;
            _isMoveConnection = false;
            var finish = _currentConnection.TryFinishConnect();
            if (finish && CheckoutMode(_currentConnection.ConnectedBaseNode))
            {
                SubscribeConnection(_currentConnection);
            }
            else
            {
                if(finish)
                    _currentConnection.Die();
                UnsubscribeConnection(_currentConnection);
            }
            _currentConnection = null;
        }
        
        private void MoveConnection()
        {
            _currentConnection.MoveConnect(Input.mousePosition);
        }

        private void ManageNewConnection()
        {
            if (_currentConnection == null)
            {
                _currentConnection = new Connection(_end, _solid, this);
            }
            _currentConnection.MoveConnect(Input.mousePosition);
        }
    }
}
