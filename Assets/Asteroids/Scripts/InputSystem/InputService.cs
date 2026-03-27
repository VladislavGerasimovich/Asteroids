using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string JoystickHorizontal = "JoystickHorizontal";
        protected const string JoystickVertical = "JoystickVertical";
        
        public abstract Vector2 TempAxis { get; }
    }
}