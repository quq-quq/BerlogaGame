using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Detectable : MonoBehaviour
    {
        [SerializeField] private bool _isDetectable = true;

        public Action<bool> StateChangedEvent;
        public bool IsDetectable
        {
            get => _isDetectable;
            set 
            {
                _isDetectable = value;
                StateChangedEvent?.Invoke(_isDetectable);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            StateChangedEvent?.Invoke(_isDetectable);
        }
#endif
    }
}
