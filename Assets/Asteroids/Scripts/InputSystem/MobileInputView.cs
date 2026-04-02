using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class MobileInputView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup joystickPanelCanvasGroup;
        [SerializeField] private MobileJoystick mobileJoystick;
        [SerializeField] private TrackableButton firstGunSlotButton;
        [SerializeField] private TrackableButton secondGunSlotButton;
        
        public MobileJoystick MobileJoystick => mobileJoystick;
        public TrackableButton FirstGunSlotButton => firstGunSlotButton;
        public TrackableButton SecondGunSlotButton => secondGunSlotButton;
        
        public void Show()
        {
            joystickPanelCanvasGroup.alpha = 1f;
            joystickPanelCanvasGroup.interactable = true;
            joystickPanelCanvasGroup.blocksRaycasts = true;
        }
        
        public void Hide()
        {
            joystickPanelCanvasGroup.alpha = 0f;
            joystickPanelCanvasGroup.interactable = false;
            joystickPanelCanvasGroup.blocksRaycasts = false;
        }
    }
}