using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Core.Extension;
using Core.Gameplay.SceneManagement;
using Node_System.Scripts.Node;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using Button = UnityEngine.UI.Button;

namespace Core.Gameplay.UISystem
{
    public class GlassesPanel : UIPanel
    {
        public override bool IsHided { get; protected set; } = false;
        protected override CanvasGroup CanvasGroup => _canvasGroup;
        private CanvasGroup _canvasGroup;
        private UIPanelController _uiPanelController;

        [SerializeField, Min(0.1f)] private float _sensitivity;
        [Space]
        [SerializeField] private Button _buttonClose;
        [SerializeField] private PointerCatcher _pointerCatcher;

        private List<Title> _objects = new List<Title>();
        private CinemachineTransposer _transposer;
        private Camera _mainCamera;
        private CinemachineConfiner2D _composer;
        private List<Vector3> _cameraPoints = new List<Vector3>(5);
        private Vector3 _originalOffset;
        private Vector2 _startClick;
        private bool _clicked;

        [Inject]
        private void Inject(UIPanelController panelController)
        {
            TransitionDuration = 0f;
            _canvasGroup = GetComponent<CanvasGroup>();
            base.Hide(null, false);
            _uiPanelController = panelController;
            _uiPanelController.RegisterPanel(this);
        }
        

        /*private void SetCamera()
        {
            _mainCamera = Camera.main;
            _cameraPoints.Add((_mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)) -
                               _mainCamera.transform.position).ClearZ());
            _cameraPoints.Add((_mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)) -
                               _mainCamera.transform.position).ClearZ());
            _cameraPoints.Add((_mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)) -
                               _mainCamera.transform.position).ClearZ());
            _cameraPoints.Add((_mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)) -
                               _mainCamera.transform.position).ClearZ());
            _cameraPoints.Add((_mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)) -
                               _mainCamera.transform.position).ClearZ());
            var vc = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera
                .VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
            _transposer = vc.GetCinemachineComponent<CinemachineTransposer>();
            _composer = vc.GetComponent<CinemachineConfiner2D>();
            _originalOffset = _transposer.m_FollowOffset;
        }*/
        

        private void OnEnable()
        {
            _buttonClose.onClick.AddListener(Exit);
            //_pointerCatcher.PointerDownEvent += OnPointDown;
            //_pointerCatcher.PointerUpEvent += OnPointUp;
        } 
        
        private void OnDisable()
        {
            _buttonClose.onClick.RemoveListener(Exit);
            //_pointerCatcher.PointerDownEvent -= OnPointDown;
            //_pointerCatcher.PointerUpEvent -= OnPointUp;
        }

        private void OnDestroy()
        {
            _uiPanelController.UnregisterPanel(this);
        }

        public override void Show(PanelState state, bool animate = true)
        {
            _objects = FindObjectsByType<Title>(FindObjectsSortMode.None).ToList();
            _objects.ForEach(x => x.Show());
            base.Show(state, animate);
        }

        public override void Hide(PanelState state, bool animate = true)
        {
            //_transposer.m_FollowOffset = _originalOffset;
            _objects?.ForEach(x => x.Hide());
            base.Hide(state, animate);
        }

       /* private void OnPointDown(PointerEventData data)
        {
            _clicked = true;
            _startClick = Input.mousePosition;
        }
        
        private void OnPointUp(PointerEventData data)
        {
            _clicked = false;
        }
        
        private void Update()
        {
            if (!_clicked) 
                return;
            if(_mainCamera == null)
                SetCamera();
            var delta = _startClick - (Vector2)Input.mousePosition;
            var newPosition = _mainCamera.transform.position.ClearZ() + new Vector3(delta.x/Screen.width, delta.y/Screen.height, 0f) *_sensitivity;
            print($"{newPosition} camra {_mainCamera.transform.position.ClearZ()} with delta {delta} normalaized {new Vector3(delta.x/Screen.width, delta.y/Screen.height, 0f)}");
            Vector3 damping = Vector3.zero;
            foreach (var point in _cameraPoints)
            {
                if(_composer.m_BoundingShape2D.OverlapPoint(newPosition + damping + point))
                    continue;
                damping = _composer.m_BoundingShape2D.ClosestPoint(newPosition + point) - (Vector2)(newPosition + point);
            }
            newPosition += damping;
            if (_cameraPoints.All(x => _composer.m_BoundingShape2D.OverlapPoint(newPosition + x)))
            {
                _transposer.m_FollowOffset += newPosition-_transposer.transform.position.ClearZ();
            }
        }*/
        
        private void Exit()
        {
            _uiPanelController.LoadState(PreviousPanelState);
        }
    }
}