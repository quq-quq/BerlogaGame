using Core.Extension;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core.Gameplay.UISystem
{
    public class ManualPanel : UIPanel
    {
        public override bool IsHided
        {
            get => _isHidedAwake;
            protected set => _isHidedAwake = value;
        }
        [SerializeField] private bool _isHidedAwake = true;
        
        protected override CanvasGroup CanvasGroup => _canvasGroup;
        private CanvasGroup _canvasGroup;
        
        [SerializeField] private Button _buttonExit;

        private UIPanelController _panelController;

        [Inject]
        public void Inject(UIPanelController panelController)
        {
            TransitionDuration = 0.1f;
            _panelController = panelController;
            _panelController.RegisterPanel(this);
            _canvasGroup = GetComponent<CanvasGroup>();
            transform.SetActiveForChildren(!_isHidedAwake);
        }

        private void OnEnable()
        {
            _buttonExit.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _buttonExit.onClick.RemoveListener(Exit);
        }

        private void Exit()
        {
            if (PreviousPanelState != null)
            {
                _panelController.LoadState(PreviousPanelState, this);
                HideAnimation();
            }
            _panelController.ClosePanel(this);
        }

        private void OnDestroy()
        {
            _panelController.UnregisterPanel(this);
        }
    }
}