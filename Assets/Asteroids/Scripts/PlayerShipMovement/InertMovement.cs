using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class InertMovement
    {
        private readonly float _unitsPerSecond = 0.0005f;
        private readonly float _maxSpeed = 0.0015f;
        private readonly float _maxBounceSpeed = 0.0035f;
        private readonly float _secondsToStop = 0.2f;

        private bool _hasBounced;

        public Vector2 Acceleration { get; private set; }

        public void Accelerate(Vector2 forward)
        {
            _hasBounced = false;
            
            Acceleration += forward * (_unitsPerSecond * Time.deltaTime);
            Acceleration = Vector2.ClampMagnitude(Acceleration, _maxSpeed);
        }
        
        public void Slowdown()
        {
            ReduceAcceleration();
            _hasBounced = false;
        }

        public void SlowAccelerate(Vector2 direction)
        {
            if (!_hasBounced)
            {
                Acceleration = Vector2.zero;
                Acceleration += direction * Time.deltaTime;
                Acceleration = Vector2.ClampMagnitude(Acceleration, _maxBounceSpeed);
                _hasBounced = true;
            }
            else
            {
                ReduceAcceleration();
            }
        }

        private void ReduceAcceleration()
        {
            Acceleration -= Acceleration * (Time.deltaTime / _secondsToStop);
        }
    }
}