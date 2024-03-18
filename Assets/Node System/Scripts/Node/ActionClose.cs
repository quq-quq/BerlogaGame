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
    }
}
