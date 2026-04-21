using UnityEngine;
using UnityEngine.EventSystems;

namespace Asteroids.Scripts.InputSystem
{
    public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private int dragMovementDistance = 30;
        [SerializeField] private int dragOffsetDistance = 100;
        
        private RectTransform _joystickTransform;
        private Vector2 _tempAxis;
        
        public Vector2 Axis => _tempAxis;

        private void Awake()
        {
            _joystickTransform = (RectTransform)transform;
        }

        public void OnPointerDown(PointerEventData eventData)
        { }
        
        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickTransform,
                eventData.position,
                null,
                out _tempAxis);
            _tempAxis = Vector2.ClampMagnitude(_tempAxis, dragOffsetDistance) / dragOffsetDistance;
            _joystickTransform.anchoredPosition = _tempAxis * dragMovementDistance;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickTransform.anchoredPosition = Vector2.zero;
            _tempAxis = Vector2.zero;
        }
    }
}