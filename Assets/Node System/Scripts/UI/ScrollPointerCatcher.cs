using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ScrollPointerCatcher : MonoBehaviour
    {
        [SerializeField,Min(0f)] private float _scrollSensitivity = 1;
        [SerializeField] private PointerCatcher _pointerCatcher;
        [SerializeField] private Slider _slider;
        [SerializeField] private float _maxScale;
        [SerializeField] private float _minScale;

        private RectTransform _rectPointerCatcher;
        private RectTransform _rect;
        private bool _isClicked = false;
        private Vector3 _originMousePosition;
        private Vector3 _pointerCatcherOriginLocalPosition;

        public void Awake()
        {
            _rectPointerCatcher = _pointerCatcher.GetComponent<RectTransform>();
            _rect = GetComponent<RectTransform>();
        }

        public void Start()
        { 
            _rectPointerCatcher.sizeDelta = new Vector2(_rect.rect.width, _rect.rect.height) * (1 / _minScale);
        }

        public void OnEnable()
        {
            _slider.value = (_pointerCatcher.transform.localScale.x - _minScale) / (_maxScale - _minScale);
            _pointerCatcher.PointerDownEvent += OnPointerDown;
            _pointerCatcher.PointerUpEvent += OnPointerUp;
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        public void OnDisable()
        {
            _isClicked = false;
            _pointerCatcher.PointerDownEvent -= OnPointerDown;
            _pointerCatcher.PointerUpEvent -= OnPointerUp;
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float f)
        {
            SetDeskScale(Mathf.Lerp(_minScale,_maxScale, f));
        }

        public void SetDeskScale(float f)
        {
            var newScale = new Vector3(f, f, f);
            Vector3 localScalePos = _pointerCatcher.transform.InverseTransformPoint (transform.position);
            Vector3 scaleVector = _pointerCatcher.transform.localPosition - localScalePos;
            Vector3 oldScale = _pointerCatcher.transform.localScale;
            Vector3 scaleRatio = Div(newScale, oldScale);
            _pointerCatcher.transform.localScale = newScale;
            SetAndClampLocalPositionPointerChecker(Vector3.Scale(scaleVector, scaleRatio) + localScalePos);
             Vector3 Div(Vector3 a, Vector3 b)
            {
                return new Vector3 (b.x == 0f ? 0 : a.x / b.x, b.y == 0f ? 0 : a.y / b.y, b.z == 0f ? 0 : a.z / b.z);
            }
        }

        private void OnPointerDown(PointerEventData pointerEventData)
        {
            _isClicked = true;
            _pointerCatcherOriginLocalPosition = _pointerCatcher.transform.localPosition;
            _originMousePosition = Input.mousePosition;
        }
        
        private void Update()
        {   
            _rectPointerCatcher.sizeDelta = new Vector2(_rect.rect.width, _rect.rect.height) * (1 / _minScale);
            _slider.value -= (Input.GetAxis("Mouse ScrollWheel") * _scrollSensitivity);
            if(!_isClicked) return;
            SetAndClampLocalPositionPointerChecker(_pointerCatcherOriginLocalPosition + Input.mousePosition - _originMousePosition);

        }

        private void SetAndClampLocalPositionPointerChecker(Vector3 position)
        {
            var halfOfRectWidth = _rect.rect.width / 2;
            var halfOfRectHeight = _rect.rect.height / 2;
            var halfOfPointerRectWidth = Mathf.Max(_rectPointerCatcher.sizeDelta.x  * _rectPointerCatcher.transform.localScale.x /2, halfOfRectWidth);
            var halfOfPointerRectHeight = Mathf.Max(_rectPointerCatcher.sizeDelta.y * _rectPointerCatcher.transform.localScale.y /2, halfOfRectHeight);
            position.x = 
                Mathf.Clamp(position.x, -halfOfPointerRectWidth + halfOfRectWidth, halfOfPointerRectWidth - halfOfRectWidth);
            position.y = 
                Mathf.Clamp(position.y, -halfOfPointerRectHeight + halfOfRectHeight, halfOfPointerRectHeight - halfOfRectHeight);
            _pointerCatcher.transform.localPosition = position;
        }
        
        private void OnPointerUp(PointerEventData pointerEventData)
        {            
            _isClicked = false;
        }

    } 
}