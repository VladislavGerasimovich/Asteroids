using System;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.SaveSystem;
using Asteroids.Scripts.Signals;
using Zenject;

namespace Asteroids.Scripts.PlayerHealthSystem
{
    public class PlayerHealth : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;
        private SignalBus _signalBus;

        public event Action OnReduced;
        
        public float Health { get; private set; }

        public PlayerHealth(
            CollisionsRecords collisionsRecords,
            SaveDataRepository saveDataRepository,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _collisionsRecords = collisionsRecords;
            Health = saveDataRepository.PlayerShipHealth;
        }
        
        public void Initialize()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy += ReduceHealth;
        }

        public void Dispose()
        {
            _collisionsRecords.OnPlayerCollideWithEnemy -= ReduceHealth;
        }

        private void ReduceHealth()
        {
            Health--;
            OnReduced?.Invoke();

            if (Health <= 0)
            {
                _signalBus.Fire<PlayerDiedSignal>();
            }
        }
    }
}