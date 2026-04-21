using System;
using Asteroids.Scripts.PlayerShipMovement;
using MVVM;
using UniRx;
using Zenject;

namespace Asteroids.Scripts.ViewModels
{
    public class ShipViewModel : ITickable
    {
        [Data("ShipPosition")]
        public ReactiveProperty<string> ShipPosition;
        
        [Data("ShipRotation")]
        public ReactiveProperty<string> ShipRotation;
        
        private ShipMovement _shipMovement;

        public ShipViewModel(ShipMovement shipMovement)
        {
            _shipMovement = shipMovement;
            ShipPosition = new ReactiveProperty<string>($"Position: {_shipMovement.Position.x}, {_shipMovement.Position.y}");
            ShipRotation = new ReactiveProperty<string>($"Rotation: {_shipMovement.Rotation}");
        }
        
        public void Tick()
        {
            ShipPosition.Value = $"Position: {Math.Round(_shipMovement.Position.x, 1)}, {Math.Round(_shipMovement.Position.y, 1)}";
            ShipRotation.Value = $"Rotation: {Math.Round(_shipMovement.Rotation, 0)}";
        }
    }
}