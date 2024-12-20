using System;
using Core.Extension;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Core.Gameplay.UISystem
{
    [RequireComponent(typeof(CanvasGroup), typeof(Canvas))]
    public abstract class UIPanel : MonoBehaviour
    { 
        protected float TransitionDuration = 0.3f;
        
        [SerializeField] private string _panelName;
        public string PanelName
        {
            get => _panelName;
            set => _panelName = value;
        }
        
        public PanelState PreviousPanelState { get; set; }

        public abstract bool IsHided { get; protected set; }
        
        protected abstract CanvasGroup CanvasGroup { get; }
        
        private TweenerCore<float,float,FloatOptions> _fadeTween;

        public virtual void Show(PanelState state, bool animate = true)
        {
            PreviousPanelState = state;
            transform.SetActiveForChildren(true);
            if(animate && IsHided)
                ShowAnimation();
            IsHided = false;
        }
        
        protected virtual void ShowAnimation(Action onCompleteAnimation = null)
        {
            _fadeTween?.Kill();
            _fadeTween = DOTween.To(() => CanvasGroup.alpha,
                x => CanvasGroup.alpha = x,
                1, TransitionDuration)
                .SetEase(Ease.InOutFlash)
                .SetUpdate(true)
                .OnComplete(() => onCompleteAnimation?.Invoke());
        }
        
        public virtual void Hide(PanelState state, bool animate = true)
        {
            PreviousPanelState = state;
            if(animate && !IsHided)
                HideAnimation(()=>transform.SetActiveForChildren(false));
            else
                transform.SetActiveForChildren(false);
            IsHided = true;
        }
        
        protected virtual void HideAnimation(Action onCompleteAnimation = null)
        {
            _fadeTween?.Kill();
            _fadeTween = DOTween.To(() => CanvasGroup.alpha,
                    x => CanvasGroup.alpha = x,
                    0, TransitionDuration)
                .SetEase(Ease.OutFlash)
                .SetUpdate(true)
                .OnComplete(() => onCompleteAnimation?.Invoke());
        }
    }
}