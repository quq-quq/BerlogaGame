using InterfaceNode;
using Node;
using NodeObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionGalagramOff : ActionNode
    {
        protected override void DoAction(ObjectForNode go)
        {
            if(go.TryGetComponent(out GalagramRobot g))
            {
                g.DeactivateGalagram();
            }
        }
        
        public override bool CanExecute(ObjectNode node)
        {
            var res = node.ObjectForNode.TryGetComponent(out GalagramRobot x);
            if(!res)
                Console.NewMessage($"{node.NodeName} не может выполнить действие \"{NodeName}\"");
            return res;
        }
    }
}
