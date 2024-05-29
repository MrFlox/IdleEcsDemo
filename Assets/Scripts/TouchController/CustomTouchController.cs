using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace TouchController
{
    internal class CustomTouchController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private RectTransform back;
        [SerializeField] private float _maxOffset;

        [Inject] private TouchInput _input;
        private Vector2 _firstTouch;

        private void Awake() => HideVisuals();

        public void OnPointerDown(PointerEventData eventData)
        {
            _firstTouch = eventData.position;

            ShowVisuals();

            target.transform.position = eventData.position;
            back.transform.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var direction = (eventData.position - _firstTouch).normalized;
            var length = Vector2.Distance(eventData.position, _firstTouch);
            var newPos = _firstTouch + direction * Mathf.Clamp(length, 0, _maxOffset);
            target.transform.position = newPos;

            _input.Direction = (newPos - _firstTouch) / _maxOffset;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            HideVisuals();
            _input.Direction = Vector2.zero;
        }
        
        private void ShowVisuals()
        {
            target.gameObject.SetActive(true);
            back.gameObject.SetActive(true);
        }

        private void HideVisuals()
        {
            target.gameObject.SetActive(false);
            back.gameObject.SetActive(false);
        }
    }
}