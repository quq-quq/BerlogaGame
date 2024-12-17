using Node;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class TriggerNode : BaseNode
    {
        [SerializeField] private TriggerForNode _trigger;

        public override void Boot()  
        {
            if (_trigger != null)
                _trigger.SubscribeTrigger(() => Do(null));
        }

        public override void Do(ObjectForNode go)
        {
            var t = Connector.GetConnectedNodes();
            foreach (var i in t)
            {
                i.Do(go);
            }
        }
    }
}
