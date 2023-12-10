﻿using InterfaceNode;
using Node;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public class ActionMoveLeft : ActionNodeParameter
    {
        protected override void DoAction(GameObject go)
        {
            if(go.TryGetComponent(out IHorizontalMover horizontalMover))
            {
                horizontalMover.MoveHorizontal(-Value);
            }
        }
    }
}
