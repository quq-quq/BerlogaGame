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

        public BaseConnector Connector => _connector;
        private readonly PointerCatcher _endPoint;
        private readonly RectTransform _rectSolid;
        private readonly Transform _parent;
        private readonly BaseConnector _connector;
        private readonly float _solidWidth;

        private ConnectorEnter _currentConnectEnter;
        
        public event Action<Connection> ClickedDownEvent;
        public event Action<Connection> ClickedUpEvent;
        
        private bool _isDragging;
        
        public Connection(PointerCatcher end, GameObject solid, BaseConnector connector)
        {
            var parent = connector.transform;
            _rectSolid = Object.Instantiate(solid, parent.parent.parent.parent).GetComponent<RectTransform>();
            _endPoint = Object.Instantiate(end, parent.parent.parent.parent);
            _endPoint.transform.SetAsLastSibling();
            _rectSolid.transform.SetAsFirstSibling();
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
            _isDragging = true;
            ClickedDownEvent?.Invoke(this);
        }
    
        private void OnPointerUp(PointerEventData pointerEventData)
        {
            _isDragging = false;
            ClickedUpEvent?.Invoke(this);
        }
        
        public void MoveConnect(Vector2 vector2)
        {
            if(IsConnectable(vector2, out var enter) && (_currentConnectEnter == null|| _isDragging)) 
                vector2 = enter.transform.position;
            
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

        public bool TryFinishConnect()
        {
            var able = IsConnectable(_endPoint.transform.position, out var enter);
            if (able)
            {
                FinishConnect(enter);
                return true;
            }
            Die();
            return false;
        }

        public void FinishConnect(ConnectorEnter enter)
        {
            if (_currentConnectEnter != null)
                _currentConnectEnter.Disconnect(_connector);
                
            _currentConnectEnter = enter;
            _currentConnectEnter.Connect(_connector);
            MoveConnect(enter.transform.position);
            ConnectedBaseNode = enter.Node;
        }
        
        public void SealedConnect(ConnectorEnter enter)
        {
            if (_currentConnectEnter != null)
                _currentConnectEnter.Disconnect(_connector);
                
            _currentConnectEnter = enter;
            _currentConnectEnter.SealedConnect(_connector);
            MoveConnect(enter.transform.position);
            ConnectedBaseNode = enter.Node;
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

        private bool IsConnectable(Vector2 position, out ConnectorEnter connectorEnter)
        {
            var listEnters = ConnectorEnter.GetConnectorEnters()
                .Where(x =>
                    x.IsConnectable(_connector)
                    && _connector.CheckoutMode(x.Node)
                    && x.Node != _connector.OwnerNode
                    && !x.IsSealed).ToList();
            if(listEnters.Count != 0)
            {
                var overlap = listEnters.FirstOrDefault(x =>
                {
                    Vector2 s = x.Node.transform.lossyScale;
                    Vector2 d = (Vector2)x.Node.transform.position - position;
                    d = new Vector2(d.x/s.x, d.y/s.y);
                    return x.Node.RectTransform.rect.Contains(d);
                });
                if(overlap != null)
                {
                    connectorEnter = overlap;
                    return true;
                }
                //nearest
                listEnters = listEnters.OrderBy(i => 
                    Vector2.Distance(i.transform.position, position)).ToList();
                var closest = listEnters[0];
                if (Vector2.Distance(closest.transform.position, position) * closest.Node.transform.localScale.x <= _stickDistance)
                {
                    connectorEnter = closest;
                    return true;
                }
            }
            connectorEnter = null;
            return false;
        }
    }
}
