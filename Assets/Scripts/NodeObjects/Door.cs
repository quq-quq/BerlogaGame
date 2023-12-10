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
        private Collider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            SwitchStates(_isOpen);
        }

        public void Open()
        {
            SwitchStates(true);
        }

        public void Close()
        {
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
