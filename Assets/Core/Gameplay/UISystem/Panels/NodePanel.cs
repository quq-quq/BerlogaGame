using System.Collections.Generic;
using System.Linq;
using Core.Extension;
using Node;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace Core.Gameplay.UISystem
{
    public class NodePanel : UIPanel
    {
        public override bool IsHided
        {
            get => _isHidedAwake;
            protected set => _isHidedAwake = value;
        }
        [SerializeField] private bool _isHidedAwake = true;
        
        protected override CanvasGroup CanvasGroup => _canvasGroup;
        private CanvasGroup _canvasGroup;
        
        [FormerlySerializedAs("_buttonExit")] 
        [SerializeField] private Button _exitButton;
        [FormerlySerializedAs("_buttonFastRun")] 
        [SerializeField] private Button _fastRunButton;
        [SerializeField] private string _overlayPanelName;
        [FormerlySerializedAs("_buttonManual")]
        [Space]
        [SerializeField] private Button _manualButton;
        [SerializeField] private string _manualPanelName;
        [Space]
        [SerializeField] private Button _staticDeskButton;
        [SerializeField] private GameObject _staticDesk;
        [SerializeField] private GameObject _defaultDesk;

        private UIPanelController _panelController;
        private List<BaseConnector> _baseConnectors = new List<BaseConnector>();
        private List<BaseNode> _nodes = new List<BaseNode>();
        private List<ConnectorEnter> _enters = new List<ConnectorEnter>();

        [Inject]
        public void Inject(UIPanelController panelController)
        {
            TransitionDuration = 0.15f;
            _panelController = panelController;
            _panelController.RegisterPanel(this);
            _canvasGroup = GetComponent<CanvasGroup>();
            transform.SetActiveForChildren(!_isHidedAwake);
            _enters = GetComponentsInChildren<ConnectorEnter>(true).ToList();
            _enters.ForEach(x => x.Boot());
            _baseConnectors = GetComponentsInChildren<BaseConnector>(true).ToList();
            _baseConnectors.ForEach(x => x.Boot());
            _nodes = GetComponentsInChildren<BaseNode>(true).ToList();
            _nodes.ForEach(x => x.Boot());
            _defaultDesk.SetActive(true);
            _staticDesk.SetActive(false);
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(Exit);
            _fastRunButton.onClick.AddListener(Exit);
            _manualButton.onClick.AddListener(OpenManual);
            _staticDeskButton.onClick.AddListener(SwitchDesk);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(Exit);
            _fastRunButton.onClick.RemoveListener(Exit);
            _manualButton.onClick.RemoveListener(OpenManual);
            _staticDeskButton.onClick.RemoveListener(SwitchDesk);
        }

        private void SwitchDesk()
        {
            _staticDesk.SetActive(!_staticDesk.activeSelf);
            _defaultDesk.SetActive(!_defaultDesk.activeSelf);
        }

        private void OpenManual()
        {
            _panelController.OpenPanel(_manualPanelName);
        }
        

        private void Exit()
        {
            _panelController.ClosePanel(this);
        }

        private void OnDestroy()
        {
            _panelController.UnregisterPanel(this);
        }
    }
}