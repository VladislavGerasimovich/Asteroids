using System;
using Asteroids.Scripts.PlayerShipMovement;
using MVVM;
using UniRx;
using Zenject;

namespace Asteroids.Scripts.ViewModels
{
    public class InertMovementViewModel : ITickable
    {
        [Data("ShipSpeed")]
        public ReactiveProperty<string> ShipSpeed;
        
        private InertMovement _inertMovement;
        private float _speedMultiplier;

        public void Init(InertMovement inertMovement)
        {
            _inertMovement = inertMovement;
            ShipSpeed = new ReactiveProperty<string>($"Speed: 0");
            _speedMultiplier = 10000f;
        }
        
        public void Tick()
        {
            ShipSpeed.Value = $"Speed: {Math.Round(_inertMovement.Acceleration.magnitude * _speedMultiplier, 0)}";
        }
    }
}