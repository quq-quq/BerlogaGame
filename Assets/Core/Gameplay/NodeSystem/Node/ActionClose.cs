using InterfaceNode;
using Node;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionClose : ActionNode
    {
        protected override void DoAction(ObjectForNode go)
        {
            if(go == null) return;
            
            if(go.TryGetComponent(out IOpenClose openClose))
            {
                openClose?.Close();
            }
        }

        public override bool CanExecute(ObjectNode node)
        {
            var res = node.ObjectForNode.TryGetComponent(out IOpenClose x);
            if(!res)
                Console.NewMessage($"{node.NodeName} не может выполнить действие \"{NodeName}\"");
            return res;
        }
    }
}
