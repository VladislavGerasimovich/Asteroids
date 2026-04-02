using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string JoystickHorizontal = "JoystickHorizontal";
        protected const string JoystickVertical = "JoystickVertical";
        protected const string JoystickA = "JoystickA";
        protected const string JoystickB = "JoystickB";
        
        public abstract Vector2 TempAxis { get; }
        public abstract bool IsFirstGunSlotButtonDown();
        public abstract bool IsSecondGunSlotButtonDown();
    }
}