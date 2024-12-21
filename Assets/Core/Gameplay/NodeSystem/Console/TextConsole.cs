using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Gameplay.NodeSystem.Console
{
    public class TextConsole : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bodyText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private ContentSizeFitter _fitter;
        [SerializeField] private Image _backgroundImage;

        private void Awake()
        {
            StopCoroutine(FitterFix());
            IEnumerator FitterFix()
            {
                _fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
                yield return null;
                _fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }
        }

        public void SetText(string text)
        {
            var color = Color.red;
            color.a = 0;
            _backgroundImage.color = color;
            _backgroundImage.DOColor(Color.red, 0.2f)
                .OnComplete(() => _backgroundImage.DOFade(0f, 0.2f)
                    .OnComplete(() => _backgroundImage.color = color));
            _bodyText.text = text;
            _timeText.text = System.DateTime.Now.ToString("HH:mm:ss"); 
            _fitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            _fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
        
    }
}