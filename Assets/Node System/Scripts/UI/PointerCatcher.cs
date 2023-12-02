using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PointerCatcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler , IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<PointerEventData> PointerDownEvent,
            PointerUpEvent,
            PointerMoveEvent,
            PointerEnterEvent,
            PointerExitEvent;
    
        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDownEvent?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUpEvent?.Invoke(eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            PointerMoveEvent?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnterEvent?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExitEvent?.Invoke(eventData);
        }
    }
}
