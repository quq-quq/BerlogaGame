using InterfaceNode;
using Node;
using Unity.VisualScripting;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionOpen : ActionNode<IOpenClose>
    {
        protected override void DoAction(GameObject go)
        {
            if(go.TryGetComponent(out IOpenClose openClose))
            {
                openClose.Open();
            }
        }
    }
}
