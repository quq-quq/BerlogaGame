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
            SwitchStates(true);
            SoundController.sounder.SetSound(_doorAudio, false, gameObject.name, _volume);
        }

        public void Close()
        {
            SwitchStates(false);
            SoundController.sounder.SetSound(_doorAudio, false, gameObject.name, _volume);
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
