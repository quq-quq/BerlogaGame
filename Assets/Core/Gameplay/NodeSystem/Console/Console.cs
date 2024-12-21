using System;
using System.Collections;
using Core.Gameplay.UISystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Gameplay.NodeSystem.Console
{
    public class Console : MonoBehaviour
    {
        [SerializeField] private SlideHideButton _slideHideButton;
        [SerializeField] private TextConsole _textConsolePrefab;
        [SerializeField] private Transform _contentContainer;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Image _bell;

        private void Start()
        {
            _slideHideButton.SwitchPosition();
        }

        private void OnEnable()
        {
            _slideHideButton.OnSwitch += BellSwitch;
        }

        private void OnDisable()
        {
            
            _slideHideButton.OnSwitch -= BellSwitch;
        }

        public void NewMessage(string text)
        {
            var go = Instantiate(_textConsolePrefab, _contentContainer);
            go.SetText(text);
            go.gameObject.SetActive(true);
            StartCoroutine(Scroll());
            if (_slideHideButton.IsHided)
            {
                var color = _bell.color;
                color.a = 0f;
                _bell.color = color;
                _bell.DOFade(1, 0.5f).SetEase(Ease.InOutBounce);
            }
            IEnumerator Scroll()
            {
                yield return null;
                DOTween.To(() => _scrollRect.verticalNormalizedPosition,
                    x => _scrollRect.verticalNormalizedPosition = x, 0f, 0.5f);
            }
        }

        private void BellSwitch()
        {
            if(_slideHideButton.IsHided)
                return;
            _bell.DOFade(0, 0.2f);
        }
    }
}