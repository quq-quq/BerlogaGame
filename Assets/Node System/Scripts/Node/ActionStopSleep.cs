using InterfaceNode;
using Node;
using NodeObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionStopSleep : ActionNode
    {
        protected override void DoAction(GameObject go)
        {
            if(go.TryGetComponent(out Quadrocopter q))
            {
                q.StopSleep();
            }
        }
    }
}
