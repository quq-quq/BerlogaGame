using UnityEngine;

namespace Node_System.Scripts.Node
{
    public abstract class ObjectForNode : MonoBehaviour
    {
        [SerializeField] private string _name;
        public string NodeName
        {
            get => _name;
        }
    }
}
