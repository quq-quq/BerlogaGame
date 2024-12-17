using System.Linq;
using Node_System.Scripts.Node;
using UI;
using UnityEngine;

namespace Node
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseNode : MonoBehaviour
    {
        private BaseConnector _connector;
        private RectTransform _rectTransform;
        
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
                if (_connector == null)
                {
                    _connector = GetComponentInChildren<BaseConnector>(true);
                }

                return _connector;

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
    }
}
