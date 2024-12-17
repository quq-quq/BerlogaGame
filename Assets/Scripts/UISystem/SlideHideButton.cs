using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Gameplay.UISystem
{
    [RequireComponent(typeof(RectTransform))]
    public class SlideHideButton : MonoBehaviour
    {
        private static float TimeHide = 1f;
        [SerializeField] private Button _button;
        [SerializeField] private Direction _direction;
        [SerializeField] private bool _isHided;
        
        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private Vector2 _hidePosition;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            var delta =  _direction is Direction.Left or Direction.Right? 
                _rectTransform.rect.width : _rectTransform.rect.height;
            var direction = _direction switch
            {
                Direction.Left => Vector2.left,
                Direction.Right => Vector2.right,
                Direction.Down => Vector2.down,
                Direction.Up => Vector2.up,
                _ => throw new ArgumentOutOfRangeException()
            };
            if(!_isHided)
            {
                _hidePosition = _rectTransform.anchoredPosition + direction * delta;
                _originalPosition = _rectTransform.anchoredPosition;
            }
            else
            {
                _hidePosition = _rectTransform.anchoredPosition;
                _originalPosition = _rectTransform.anchoredPosition - direction * delta;
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SwitchPosition);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchPosition);
        }

        private void SwitchPosition()
        {
           var destination = _isHided ? _originalPosition : _hidePosition;
            DOTween.To(() => _rectTransform.anchoredPosition, 
                x => _rectTransform.anchoredPosition = x,
                destination, TimeHide).SetEase(Ease.OutBounce);
            _isHided = !_isHided;
        }

        public enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }
    }
    
    
}
