using System;
using System.Collections.Generic;
using System.Linq;
using Node;
using Node_System.Scripts.Node;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ControllableConnector : BaseConnector
    {
        private PointerCatcher _pointerCatcher;
        protected bool _isClicked = false;
        protected bool _isMoveConnection = false;
        private Connection _currentConnection = null;
        
        protected override void OnAwake()
        {
            _pointerCatcher = GetComponent<PointerCatcher>();
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

        protected override bool CheckoutMode()
        {
            var node = _currentConnection.ConnectedBaseNode;
            if(node == null)
            {
                return false;
            }
            var baseType = node.GetType().BaseType;
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
            if(_connections.Any(i => i == _currentConnection))
                return;
            
            _connections.Add(_currentConnection);
            _currentConnection.ClickedDownEvent += OnClickDownConnection;
            _currentConnection.ClickedUpEvent += OnClickUpConnection;
        }
        
        private void UnsubscribeConnection(Connection connect)
        {
            _connections.Remove(_currentConnection);
            _currentConnection.ClickedDownEvent -= OnClickDownConnection;
            _currentConnection.ClickedUpEvent -= OnClickUpConnection;
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
            var finish = _currentConnection.FinishConnect();
            if (finish && CheckoutMode())
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
