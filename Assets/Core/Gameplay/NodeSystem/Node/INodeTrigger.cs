using System;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    // there more right way is use interface, BUT unity can't Serialize interfaces
    public abstract class TriggerForNode : MonoBehaviour
    {
        [SerializeField] private Title _title;
        public abstract void SubscribeTrigger(Action action);

        public virtual void SetTitle(string title)
        {
            _title.SetText(title);
        }
    }
}
