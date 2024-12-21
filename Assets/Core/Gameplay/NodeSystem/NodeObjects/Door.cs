using System;
using DG.Tweening;
using InterfaceNode;
using Node_System.Scripts.Node;
using UnityEngine;

namespace NodeObjects
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Transform))]
    public class Door : ObjectForNode, IOpenClose
    {
        [SerializeField] private GameObject _openSkin;
        [SerializeField] private GameObject _closeSkin;
        [SerializeField] private bool _isOpen;
        [SerializeField] private AudioClip _doorAudio;
        [SerializeField] private float _volume;
        [SerializeField] private float _distanceUp;
        [SerializeField] private float _durationForMove;
        private Collider2D _collider;
        private float _currentDistance;

        public bool IsOpen => _isOpen;

        public event Action OpenEvent;
        public event Action CloseEvent;

        private void Awake()
        {
            _currentDistance = transform.localPosition.y;
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            SwitchStates(_isOpen);
            Move(_isOpen);
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
            if (_isOpen != isOpen)
            {
                Move(isOpen);                
            }
            _isOpen = isOpen;
        }

        private void Move(bool isOpen)
        {
            transform.DOKill();
            if (isOpen)
            {
                transform.DOLocalMoveY(_currentDistance + _distanceUp, _durationForMove).SetEase(Ease.Linear);
            }
            else
            {
                transform.DOLocalMoveY(_currentDistance, _durationForMove);
            }

            SoundController.sounder.SetSound(_doorAudio, false, gameObject.name, _volume);
        }
    
    }
}
