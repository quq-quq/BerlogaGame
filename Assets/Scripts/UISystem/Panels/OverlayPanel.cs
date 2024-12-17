using System;
using Core.Extension;
using Core.Gameplay.SceneManagement;
using Save_files.Scripts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core.Gameplay.UISystem
{
    public class OverlayPanel : UIPanel
    {
        protected override CanvasGroup CanvasGroup => _canvasGroup;
        private CanvasGroup _canvasGroup;
        public override bool IsHided
        {
            get => _isHidedAwake;
            protected set => _isHidedAwake = value;
        }

        [SerializeField] private bool _isHidedAwake;
        
        [Header("Buttons")]
        [SerializeField] private Button _pauseButton;
        [SerializeField] private string _pauseNamePanel;
        [Space]
        [SerializeField] private Button _soundButton;
        [SerializeField] private Image _soundImage;
        [SerializeField] private Sprite _mute;
        [SerializeField] private Sprite _unmute;
        [Space]
        [SerializeField] private Button _restartButton;
        [Space]
        [SerializeField] private Button _nodeButton;
        [SerializeField] private string _nodeNamePanel;
        [Space]
        [SerializeField] private Button _manualButton;
        [SerializeField] private string _manualNamePanel;

    
        private bool _isMuted;
        private SceneLoader _sceneLoader;
        private UIPanelController _uiPanelController;
    
        [Inject]
        private void Inject(SceneLoader sceneLoader, UIPanelController panelController)
        {
            TransitionDuration = 0.8f;
            _canvasGroup = GetComponent<CanvasGroup>();
            _uiPanelController = panelController;
            _uiPanelController.RegisterPanel(this);
            transform.SetActiveForChildren(!_isHidedAwake);
            _canvasGroup.alpha = _isHidedAwake ? 0 : 1;
            
            _sceneLoader = sceneLoader;
            _isMuted = Saver.Data.IsMute;
            AudioListener.pause = _isMuted;
            UpdateView();
        }

        private void OnEnable()
        {
            _soundButton?.onClick.AddListener(SwitchSound);
            _pauseButton?.onClick.AddListener(SwitchToPause);
            _restartButton?.onClick.AddListener(RestartScene);
            _nodeButton?.onClick.AddListener(SwitchToNode);
            _manualButton?.onClick.AddListener(SwitchToManual);
        }

        private void OnDisable()
        {
            _soundButton?.onClick.RemoveListener(SwitchSound);
            _pauseButton?.onClick.RemoveListener(SwitchToPause);
            _restartButton?.onClick.RemoveListener(RestartScene);
            _nodeButton?.onClick.RemoveListener(SwitchToNode);
            _manualButton?.onClick.RemoveListener(SwitchToManual);
        }

        private void SwitchToPause()
        {
            _uiPanelController.OpenPanel(_pauseNamePanel);
        }
        
        private void SwitchToNode()
        {
            _uiPanelController.OpenPanel(_nodeNamePanel);
        }
        
        private void SwitchToManual()
        {
            _uiPanelController.OpenPanel(_manualNamePanel);
        }


        private void SwitchSound()
        {
            _isMuted = !_isMuted;
            AudioListener.pause = _isMuted;
            Saver.Data.IsMute = _isMuted;
            Saver.Save();
            UpdateView();
        }

        private void Update()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            _isMuted = Saver.Data.IsMute;
            _soundImage.sprite = _isMuted? _mute : _unmute;
        }

        public void RestartScene()
        {
            _sceneLoader.LoadScene(_sceneLoader.GetCurrentScene());
        }

        private void OnDestroy()
        {
            _uiPanelController.UnregisterPanel(this);
        }
    }
}
