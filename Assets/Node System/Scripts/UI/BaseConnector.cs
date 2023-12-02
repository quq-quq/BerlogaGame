using System;
using System.Collections.Generic;
using System.Linq;
using Node;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(PointerCatcher))]
    public class BaseConnector : MonoBehaviour
    {
        [SerializeField] private BaseNode _node;
        
        public BaseNode OwnerNode
        {
            get => _node;
        }
        
        [SerializeField] private PointerCatcher _end;
        [SerializeField] private GameObject _solid;
        private PointerCatcher _pointerCatcher;
        protected bool _isClicked = false;
        protected bool _isMoveConnection = false;
        private Connection _currentConnection = null;
        private List<Connection> _connections = new List<Connection>();
        
        private void Awake()
        {
            _end.gameObject.SetActive(false);
            _solid.gameObject.SetActive(false);
            _pointerCatcher = GetComponent<PointerCatcher>();
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

        private void LateUpdate()
        {
            /// is NOT good idea. Doing like that. But i have no time.
            _connections.ForEach(i => i.UpdatePosition());
            ///is NOT good idea. Doing like that. But i have no time.
            
            if (_isMoveConnection)
            {
                MoveConnection();
                return;
            }
            
            if(!_isClicked) return;
            ManageNewConnection();
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
            if (_currentConnection.FinishConnect())
            {
                SubscribeConnection(_currentConnection);
            }
            else
            {
                UnsubscribeConnection(_currentConnection);
            }
            _currentConnection = null;
        }

        public List<BaseNode> GetConnectedNodes()
        {
            var r = new List<BaseNode>();
            _connections.ForEach(i => r.Add(i.ConnectedBaseNode));
            return r;
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
