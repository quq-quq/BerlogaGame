using InterfaceNode;
using Node;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionMoveLeft : ActionNodeParameter
    {
        protected override void DoAction(ObjectForNode go)
        {
            if(go.TryGetComponent(out IHorizontalMover horizontalMover))
            {
                horizontalMover.MoveHorizontal(-Value);
            }
        }
        
        public override bool CanExecute(ObjectNode node)
        {
            var res = node.ObjectForNode.TryGetComponent(out IHorizontalMover x);
            if(!res)
                Console.NewMessage($"{node.NodeName} не может выполнить действие \"{NodeName}\"");
            return res;
        }
    }
}
