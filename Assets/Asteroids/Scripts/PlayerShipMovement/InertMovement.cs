using Asteroids.Scripts.SaveSystem;
using UnityEngine;

namespace Asteroids.Scripts.PlayerShipMovement
{
    public class InertMovement
    {
        private readonly float _unitsPerSecond;
        private readonly float _maxSpeed;
        private readonly float _maxBounceSpeed;
        private readonly float _secondsToStop;

        private bool _hasBounced;

        public Vector2 Acceleration { get; private set; }

        public InertMovement(SaveDataRepository saveDataRepository)
        {
            _unitsPerSecond = saveDataRepository.UnitsPerSecond;
            _maxSpeed = saveDataRepository.MaxSpeed;
            _maxBounceSpeed = saveDataRepository.MaxBounceSpeed;
            _secondsToStop = saveDataRepository.SecondsToStop;
        }

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