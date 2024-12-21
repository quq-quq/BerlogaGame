using System;
using Node;
using UI;
using UnityEngine;
using Console = Core.Gameplay.NodeSystem.Console.Console;

namespace Node_System.Scripts.Node
{
    public abstract class ActionNode : BaseNode
    {
        protected Console Console;
        public override void Boot()
        {
            Enter.OnEnter += CheckAction;
        }

        private void OnDestroy()
        {
            Enter.OnEnter  -= CheckAction;
        }

        public override void Do(ObjectForNode go)
        {
            DoAction(go);
            Connector.GetConnectedNodes().ForEach(i => i.Do(go));
        }

        private void CheckAction(BaseConnector connection)
        {
            if (Console == null)
                Console = FindObjectOfType<Console>();
            Connector.GetConnectedNodes().ForEach(x =>
            {
                if(x is ActionNode action)
                    action.CheckAction(connection);
            });
            var node = connection.OwnerNode;
            if (node is ObjectNode on)
            {
                CanExecute(on);
            }
            else
            {
                var l = node.GetConnectedNodeOfType<ObjectNode>();
                foreach (var n in l)
                {
                    CanExecute(n);
                }
            }
        }

        protected abstract void DoAction(ObjectForNode go);

        public abstract bool CanExecute(ObjectNode node);
    }
}
