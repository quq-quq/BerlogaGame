using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class FollowMouse : MonoBehaviour
    {
        [SerializeField] private PointerCatcher _pointerCatcher;
        [SerializeField] private Vector2 _offsetBarrierPixels; 
        private RectTransform _barrier;
        private bool _isClicked = false;
        private RectTransform _rect;
        private Vector3 _originMousePosition;
        private Vector3 _deltaBetweenOriginMouseAndOriginPointer;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _barrier = transform.parent.GetComponent<RectTransform>();
        }

        public void Init(RectTransform rectBarrier)
        {
            _barrier = rectBarrier;
        }
        
        public void OnEnable()
        {
            _pointerCatcher.PointerDownEvent += OnPointerDown;
            _pointerCatcher.PointerUpEvent += OnPointerUp;
        }

        public void OnDisable()
        {
            _isClicked = false;
            _pointerCatcher.PointerDownEvent -= OnPointerDown;
            _pointerCatcher.PointerUpEvent -= OnPointerUp;
        }
        

        private void OnPointerDown(PointerEventData obj)
        {
            _originMousePosition = Input.mousePosition;
            _deltaBetweenOriginMouseAndOriginPointer = _pointerCatcher.transform.position - _originMousePosition;
            _isClicked = true;
        }
        
        private void OnPointerUp(PointerEventData obj)
        {
            _isClicked = false;
        }

        private void Update()
        {
            if(!_isClicked) return;
            
            var deltaPositionWithPointer = transform.position - _pointerCatcher.transform.position;
            var newPosition = Input.mousePosition + deltaPositionWithPointer + _deltaBetweenOriginMouseAndOriginPointer;
            if(_barrier != null)
            {
                var halfBarrierWight = _barrier.rect.width * _barrier.transform.lossyScale.x / 2;
                var halfBarrierHeight = _barrier.rect.height * _barrier.transform.lossyScale.y / 2;
                var localLimitWight = halfBarrierWight  - _rect.rect.width  * transform.lossyScale.x / 2 - _offsetBarrierPixels.x * _barrier.transform.lossyScale.x;
                var localLimitHeight = halfBarrierHeight - _rect.rect.height  * transform.lossyScale.y / 2 - _offsetBarrierPixels.y * _barrier.transform.lossyScale.y;
                newPosition.x = Mathf.Clamp(newPosition.x, _barrier.transform.position.x - localLimitWight,
                    _barrier.transform.position.x + localLimitWight);
                newPosition.y = Mathf.Clamp(newPosition.y, _barrier.transform.position.y - localLimitHeight,
                    _barrier.transform.position.y + localLimitHeight);
            }
            
            transform.position = newPosition;
        }
    }
}
