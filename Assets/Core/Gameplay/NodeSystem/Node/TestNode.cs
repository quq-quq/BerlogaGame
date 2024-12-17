using Node_System.Scripts.Node;
using UnityEngine;

namespace Node
{
    public class TestNode : BaseNode
    {
        [SerializeField] public string _print;

        public override void Boot()
        {
        }

        public override void Do(ObjectForNode go)
        {
            print(_print);
        }
    }
}
