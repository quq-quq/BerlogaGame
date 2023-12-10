using InterfaceNode;
using Node;
using NodeObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionInvokeSleep : ActionNode
    {
        protected override void DoAction(GameObject go)
        {
            if(go.TryGetComponent(out Quadrocopter q))
            {
                q.StartSleep();
            }
        }
    }
}
