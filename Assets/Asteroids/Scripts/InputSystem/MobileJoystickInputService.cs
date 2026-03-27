using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class MobileJoystickInputService : InputService
    {
        private MobileJoystick _mobileJoystick;
        
        public MobileJoystickInputService(MobileJoystick mobileJoystick)
        {
            _mobileJoystick = mobileJoystick;
        }

        public override Vector2 TempAxis => _mobileJoystick.Axis;
    }
}