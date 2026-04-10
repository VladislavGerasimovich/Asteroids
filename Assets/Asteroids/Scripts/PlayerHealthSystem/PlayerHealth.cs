using System;
using Asteroids.Scripts.OwnPhysics;
using Zenject;

namespace Asteroids.Scripts.PlayerHealthSystem
{
    public class PlayerHealth : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;

        public event Action OnHealthChanged;
        
        public float Health { get; private set; }

        public PlayerHealth(CollisionsRecords collisionsRecords)
        {
            _collisionsRecords = collisionsRecords;
            Health = 3;
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
            OnHealthChanged?.Invoke();
        }
    }
}