using System;
using Node;
using UI;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Node_System.Scripts.Node
{
    public abstract class ActionNode : BaseNode
    {
        public override void Do(GameObject go)
        {
            DoAction(go);
            Connector.GetConnectedNodes().ForEach(i => i.Do(go));
        }

        protected abstract void DoAction(GameObject go);
    }
}
