using System;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;

namespace NodeObjects
{
    [RequireComponent(typeof(Collider2D))]
    public class Door : ObjectForNode, IOpenClose
    {
        [SerializeField] private GameObject _openSkin;
        [SerializeField] private GameObject _closeSkin;
        [SerializeField] private bool _isOpen;

        public bool IsOpen => _isOpen;

        public event Action OpenEvent;
        public event Action CloseEvent;

        [SerializeField] private AudioClip _doorAudio;
        [SerializeField] private float _volume;
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            SwitchStates(_isOpen);
        }

        public void Open()
        {
            OpenEvent?.Invoke();
            SwitchStates(true);
        }

        public void Close()
        {
            CloseEvent?.Invoke();
            SwitchStates(false);
        }

        private void SwitchStates(bool isOpen)
        {
            _collider.enabled = !isOpen;
            _openSkin.SetActive(isOpen);
            _closeSkin.SetActive(!isOpen);
            _isOpen = isOpen;
        }
    
    }
}
