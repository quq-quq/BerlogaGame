using UnityEngine;

namespace Node
{
    public class TestNode : BaseNode
    {
        public override void Do(GameObject go)
        {
            print(NameNode);
        }
    }
}
