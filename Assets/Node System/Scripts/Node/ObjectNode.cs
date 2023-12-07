using Node;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ObjectNode : BaseNode
    {
        [SerializeField] private GameObject _gameObject;

        public override void Do(GameObject go)
        {
            Connector.GetConnectedNodes().ForEach(i => i.Do(_gameObject));
        }
    }
}
