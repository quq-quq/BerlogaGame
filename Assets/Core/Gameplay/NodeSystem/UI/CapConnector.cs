using Node;

namespace UI
{
    public class CapConnector : BaseConnector
    {

        protected override void OnAwake()
        {
        }

        protected override void OnLateUpdate()
        {
        }

        public override bool CheckoutMode(BaseNode node)
        {
            return true;
        }
    }
}
