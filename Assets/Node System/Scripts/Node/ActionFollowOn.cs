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
    }
}
