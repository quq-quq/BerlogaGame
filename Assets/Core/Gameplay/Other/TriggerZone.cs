using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerZone : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEnter,
            TriggerExit,
            TriggerStay;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit?.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerStay?.Invoke(other);
        }
    }
}
