using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Node_System.Scripts.Node
{
    public abstract class ObjectForNode : MonoBehaviour
    {
        [SerializeField] private Title _title;

        public void PrepareText(string text)
        {
            _title.SetText(text);
            _title.Hide();
        }

        public void ShowTitle()
        {
            _title.Show();
        }
        
        public void HideTitle()
        {
            _title.Hide();
        }
    }
}
