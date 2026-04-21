using UnityEngine;

namespace Asteroids.Scripts.InputSystem
{
    public class MobileJoystickInputService : InputService
    {
        private MobileInputView _mobileInputView;
        
        public MobileJoystickInputService(MobileInputView mobileInputView)
        {
            _mobileInputView = mobileInputView;
        }

        public override Vector2 TempAxis => _mobileInputView.MobileJoystick.Axis;
        public override bool IsFirstGunSlotButtonDown() => _mobileInputView.FirstGunSlotButton.IsPressed;
        public override bool IsSecondGunSlotButtonDown() => _mobileInputView.SecondGunSlotButton.IsPressed;
    }
}