using System;
using System.Linq;
using Node;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace UI
{
    public class Connection
    {

        private static float _stickDistance = 30f;
        public BaseNode ConnectedBaseNode
        {
            get;
            private set;
        }
        private readonly PointerCatcher _endPoint;
        private readonly RectTransform _rectSolid;
        private readonly Transform _parent;
        private readonly BaseConnector _connector;
        private readonly float _solidWidth;

        private ConnectorEnter _currentConnectEnter;
        
        public event Action<Connection> ClickedDownEvent;
        public event Action<Connection> ClickedUpEvent;

        public bool IsClicked { get; private set; }

        public Connection(PointerCatcher end, GameObject solid, BaseConnector connector)
        {
            var parent = connector.transform;
            _rectSolid = Object.Instantiate(solid, parent.parent).GetComponent<RectTransform>();
            _endPoint = Object.Instantiate(end, parent.parent);
            _rectSolid.gameObject.SetActive(true);
            _endPoint.gameObject.SetActive(true);
            _parent = parent;
            _connector = connector;
            _solidWidth = _rectSolid.sizeDelta.x;
            _endPoint.PointerUpEvent += OnPointerUp;
            _endPoint.PointerDownEvent += OnPointerDown;
            parent.SetAsLastSibling();
        } 
        
        private void OnPointerDown(PointerEventData pointerEventData)
        {
            IsClicked = true;
            ClickedDownEvent?.Invoke(this);
        }
    
        private void OnPointerUp(PointerEventData pointerEventData)
        {
            IsClicked = false;
            ClickedUpEvent?.Invoke(this);
        }
        
        public void MoveConnect(Vector2 vector2)
        {
            var delta = (vector2 - (Vector2) _parent.position); 
            _endPoint.transform.position = _parent.position + (Vector3) delta;
            var newPosition = _parent.position + (Vector3) (delta / 2);
            _rectSolid.position = newPosition;
            var angleZ = Vector2.Angle(delta, delta.y > 0? Vector2.right : Vector2.left) + 90;
            _rectSolid.rotation = Quaternion.Euler(0,0, angleZ);
            var scale = _rectSolid.transform.lossyScale.x;
            _rectSolid.sizeDelta = new Vector2(_solidWidth ,delta.magnitude / scale);
        }

        public void UpdatePosition()
        {
            MoveConnect(_currentConnectEnter.transform.position);
        }

        public bool FinishConnect()
        {
            var listEnters = ConnectorEnter.GetConnectorEnters();
            if(listEnters.Count != 0)
            {
                listEnters = listEnters.OrderBy(i => Vector2.Distance(i.transform.position, _endPoint.transform.position)).ToList();
                var closest = listEnters[0];
                if (Vector2.Distance(closest.transform.position, _endPoint.transform.position) <= _stickDistance 
                    && closest.Connect(_connector) && closest.Node != _connector.OwnerNode)
                {
                    if(_currentConnectEnter != null)
                        _currentConnectEnter.Disconnect(_connector);
                    _currentConnectEnter = closest;
                    
                    MoveConnect(closest.transform.position);
                    ConnectedBaseNode = closest.Node;
                    return true;
                }
            }
            Die();
            return false;
        }

        public void Die()
        {
            if(_currentConnectEnter != null)
                _currentConnectEnter.Disconnect(_connector);
            
            _endPoint.PointerUpEvent -= OnPointerUp;
            _endPoint.PointerDownEvent -= OnPointerDown;
            Object.Destroy(_rectSolid.gameObject);
            Object.Destroy(_endPoint.gameObject);
        }
            
    }
}
