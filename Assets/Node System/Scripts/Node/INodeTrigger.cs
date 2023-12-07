using System;
using UnityEngine;

namespace Node_System.Scripts.Node
{
    public abstract class TriggerForNode : MonoBehaviour
    {
        public abstract void SubscribeTrigger(Action action);
    }
}
