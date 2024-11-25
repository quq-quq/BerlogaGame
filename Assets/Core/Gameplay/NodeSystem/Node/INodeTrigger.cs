using System;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    // there more right way is use interface, BUT unity can't Serialize interfaces
    public abstract class TriggerForNode : MonoBehaviour
    {
        public abstract void SubscribeTrigger(Action action);
    }
}
