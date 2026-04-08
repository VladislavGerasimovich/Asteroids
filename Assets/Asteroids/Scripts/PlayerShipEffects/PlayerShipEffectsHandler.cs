using System;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShip;
using Zenject;

namespace Asteroids.Scripts.PlayerShipEffects
{
    public class PlayerShipEffectsHandler : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;
        private ShipMovement _shipMovement;

        public PlayerShipEffectsHandler(CollisionsRecords collisionsRecords, ShipMovement shipMovement)
        {
            _shipMovement = shipMovement;
            _collisionsRecords = collisionsRecords;
        }

        public void Initialize()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy += ShowInvincible;
        }

        public void Dispose()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy -= ShowInvincible;
        }

        private void ShowInvincible()
        {
            throw new NotImplementedException();
        }
    }
}