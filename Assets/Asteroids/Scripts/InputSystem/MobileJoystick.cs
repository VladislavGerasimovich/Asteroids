using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asteroids.Scripts.PlayerShip
{
    public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private float dragThreshold = 0.6f;
        [SerializeField] private int dragMovementDistance = 30;
        [SerializeField] private int dragOffsetDistance = 100;
        
        private RectTransform joystickTransform;
        private Vector2 _tempAxis;
        
        public event Action<Vector2> OnMove;

        public Vector2 Axis => _tempAxis;


        private void Awake()
        {
            joystickTransform = (RectTransform)transform;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystickTransform,
                eventData.position,
                null,
                out _tempAxis);
            _tempAxis = Vector2.ClampMagnitude(_tempAxis, dragOffsetDistance) / dragOffsetDistance;
            joystickTransform.anchoredPosition = _tempAxis * dragMovementDistance;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            joystickTransform.anchoredPosition = Vector2.zero;
            _tempAxis = Vector2.zero;
            OnMove?.Invoke(Vector2.zero);
        }
    }
}