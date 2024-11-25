using InterfaceNode;
using Node;
using Unity.VisualScripting;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionOpen : ActionNode
    {
        protected override void DoAction(ObjectForNode go)
        {
            if(go.TryGetComponent(out IOpenClose openClose))
            {
                openClose.Open();
            }
        }
    }
}
