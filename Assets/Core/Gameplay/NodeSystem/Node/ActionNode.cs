using Node;

namespace Node_System.Scripts.Node
{
    public abstract class ActionNode : BaseNode
    {
        public override void Boot()
        {
        }

        public override void Do(ObjectForNode go)
        {
            DoAction(go);
            Connector.GetConnectedNodes().ForEach(i => i.Do(go));
        }

        protected abstract void DoAction(ObjectForNode go);
    }
}
