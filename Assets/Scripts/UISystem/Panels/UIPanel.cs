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
        
        public UIPanel PreviousPanel { get; set; }

        public abstract bool IsHided { get; protected set; }
        
        protected abstract CanvasGroup CanvasGroup { get; }
        
        private TweenerCore<float,float,FloatOptions> _fadeTween;

        [ContextMenu("Show")]
        public virtual void Show()
        {
            if(!IsHided) return;
            _fadeTween?.Kill();
            transform.SetActiveForChildren(true);
            _fadeTween = DOTween.To(() => CanvasGroup.alpha,
                x => CanvasGroup.alpha = x,
                1, TransitionDuration).SetEase(Ease.InOutFlash).SetUpdate(true);
            IsHided = false;
        }

        [ContextMenu("Hide")]
        public virtual void Hide()
        {
            if (IsHided) return;
            _fadeTween?.Kill();
            _fadeTween = DOTween.To(() => CanvasGroup.alpha,
                    x => CanvasGroup.alpha = x,
                    0, TransitionDuration).SetEase(Ease.OutFlash).SetUpdate(true)
                .OnComplete(() => transform.SetActiveForChildren(false));
            IsHided = true;
            PreviousPanel = null;
        }
    }
}