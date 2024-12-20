using InterfaceNode;
using Node;
using NodeObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionFollowOn : ActionNode
    {
        protected override void DoAction(ObjectForNode go)
        {
            if(go.TryGetComponent(out Quadrocopter q))
            {
                q.StartFollow();
            }
        }
        
        public override bool CanExecute(ObjectNode node)
        {
            var res = node.ObjectForNode.TryGetComponent(out Quadrocopter x);
            if(!res)
                Console.NewMessage($"{node.NodeName} не может выполнить действие \"{NodeName}\"");
            return res;
        }
    }
}
