using System;
using Core.Extension;
using Core.Gameplay.SceneManagement;
using Save_files.Scripts;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Core.Gameplay.UISystem
{
    public class PausePanel : UIPanel
    {
        [SerializeField] private bool _isHidedAwake;
        public override bool IsHided
        {
            get => _isHidedAwake;
            protected set => _isHidedAwake = value;
        }
        
        [Header("Buttons")]
        [SerializeField] private Button _resumeButton;
        [Space]
        [SerializeField] private Button _exitButton;
        [SerializeField] private SceneData _menuScene;
        [Space]
        [SerializeField] private Button _soundButton;
        [SerializeField] private Image _soundImage;
        [SerializeField] private Sprite _mute;
        [SerializeField] private Sprite _unmute;

        protected override CanvasGroup CanvasGroup => _canvasGroup;
        private CanvasGroup _canvasGroup;
        
        private SceneLoader _sceneLoader;
        private UIPanelController _uiPanelController;
        
        private bool _isMuted;


        [Inject]
        private void Inject(SceneLoader sceneLoader, UIPanelController panelController)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _sceneLoader = sceneLoader;
            _uiPanelController = panelController;
            _uiPanelController.RegisterPanel(this);
            _canvasGroup = GetComponent<CanvasGroup>();
            transform.SetActiveForChildren(!_isHidedAwake);
            _canvasGroup.alpha = _isHidedAwake ? 0 : 1;
            
            _isMuted = Saver.Data.IsMute;
            AudioListener.pause = _isMuted;
            UpdateView();
        }

        public override void Show()
        {
            Time.timeScale = 0f;
            base.Show();
        }

        public override void Hide()
        {
            Time.timeScale = 1f;
            base.Hide();
        }

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(Resume);
            _exitButton.onClick.AddListener(Exit);
            _soundButton.onClick.AddListener(SwitchSound);

        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(Resume);
            _exitButton.onClick.RemoveListener(Exit);
            _soundButton.onClick.RemoveListener(SwitchSound);
        }

        private void Update()
        {
            UpdateView();
        }

        private void OnDestroy()
        {
            _uiPanelController.UnregisterPanel(this);
        }
        
        private void SwitchSound()
        {
            _isMuted = !_isMuted;
            AudioListener.pause = _isMuted;
            Saver.Data.IsMute = _isMuted;
            Saver.Save();
            UpdateView();
        }
        
        private void UpdateView()
        {
            _isMuted = Saver.Data.IsMute;
            _soundImage.sprite = _isMuted? _mute : _unmute;
        }

        private void Resume() => _uiPanelController.ClosePanel(this);
        private void Exit() => _sceneLoader.LoadScene(_menuScene);
    }
}