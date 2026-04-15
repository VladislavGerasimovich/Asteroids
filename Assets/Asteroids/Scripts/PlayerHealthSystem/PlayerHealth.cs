using System;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.SaveSystem;
using Zenject;

namespace Asteroids.Scripts.PlayerHealthSystem
{
    public class PlayerHealth : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;

        public event Action OnReduced;
        
        public float Health { get; private set; }

        public PlayerHealth(CollisionsRecords collisionsRecords, DataManager dataManager)
        {
            _collisionsRecords = collisionsRecords;
            dataManager.LoadProgressOrInitNew();
            Health = dataManager.PlayerShipHealth;
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
        }
    }
}