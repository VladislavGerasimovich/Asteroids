using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class GamepadInputService : InputService
    {
        private Vector2 _tempAxis;
        
        public override Vector2 TempAxis
        {
            get
            {
                _tempAxis = new Vector2(Input.GetAxis(JoystickHorizontal), Input.GetAxis(JoystickVertical));

                return _tempAxis;
            }
        }
    }
}