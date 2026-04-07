using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class InertMovement
    {
        private readonly float _unitsPerSecond = 0.0005f;
        private readonly float _maxSpeed = 0.0015f;
        private readonly float _secondsToStop = 0.2f;

        public Vector2 Acceleration { get; private set; }

        public void Accelerate(Vector2 forward)
        {
            Acceleration += forward * (_unitsPerSecond * Time.deltaTime);
            Acceleration = Vector2.ClampMagnitude(Acceleration, _maxSpeed);
        }
        
        public void Slowdown()
        {
            Acceleration -= Acceleration * (Time.deltaTime / _secondsToStop);
        }
    }
}