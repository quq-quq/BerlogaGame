using InterfaceNode;
using Node;
using NodeObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionGalagramOff : ActionNode
    {
        protected override void DoAction(GameObject go)
        {
            if(go.TryGetComponent(out GalagramRobot g))
            {
                g.DeactivateGalagram();
            }
        }
    }
}
