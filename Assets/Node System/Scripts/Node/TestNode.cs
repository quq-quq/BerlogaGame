using UnityEngine;

namespace Node
{
    public class TestNode : BaseNode
    {
        [SerializeField] public string _print;
        public override void Do(GameObject go)
        {
            print(_print);
        }
    }
}
