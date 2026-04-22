using System;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShipMovement;
using Asteroids.Scripts.Spawners;
using Asteroids.Scripts.ViewFactories.Effects;
using Zenject;

namespace Asteroids.Scripts.PlayerShipEffects
{
    public class PlayerShipEffectsHandler : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;
        private ShipMovement _shipMovement;
        private EffectsSpawner _effectsSpawner;

        public PlayerShipEffectsHandler(
            CollisionsRecords collisionsRecords,
            ShipMovement shipMovement,
            EffectsSpawner effectsSpawner)
        {
            _effectsSpawner = effectsSpawner;
            _shipMovement = shipMovement;
            _collisionsRecords = collisionsRecords;
        }

        public void Initialize()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy += ShowInvulnerability;
        }

        public void Dispose()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy -= ShowInvulnerability;
        }

        private void ShowInvulnerability()
        {
            _effectsSpawner.CreateView(Effects.Invulnerability, _shipMovement);
        }
    }
}