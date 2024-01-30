using System;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;

namespace NodeObjects
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    public class Door : ObjectForNode, IOpenClose
    {
        [SerializeField] private GameObject _openSkin;
        [SerializeField] private GameObject _closeSkin;
        [SerializeField] private bool _isOpen;
        [SerializeField] private AudioClip _doorAudio;
        private Collider2D _collider;
        private AudioSource _audioSource;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _audioSource = GetComponent<AudioSource>();

            SwitchStates(_isOpen);

            _audioSource.loop = false;
            _audioSource.clip = _doorAudio;

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

            _audioSource.Play();
        }
    
    }
}
