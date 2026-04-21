using System;
using System.Collections.Generic;
using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.SaveSystem;
using Zenject;

namespace Asteroids.Scripts.RewardSystem
{
    public class RewardHandler : IInitializable, IDisposable
    {
        private Dictionary<EnemyType, int> _enemyRewards;
        private CollisionsRecords _collisionsRecords;

        public event Action OnCountChanged;
        
        public int RewardCount { get; private set; }
        
        public RewardHandler(CollisionsRecords collisionsRecords, SaveDataRepository saveDataRepository)
        {
            _collisionsRecords = collisionsRecords;
            
            _enemyRewards = new Dictionary<EnemyType, int>()
            {
                { EnemyType.Ufo, saveDataRepository.UfoReward },
                { EnemyType.Asteroid, saveDataRepository.AsteroidReward },
                { EnemyType.PartOfAsteroid, saveDataRepository.PartOfAsteroidReward },
            };
        }

        public void Initialize()
        {
            _collisionsRecords.OnEnemyDied += Add;
        }

        public void Dispose()
        {
            _collisionsRecords.OnEnemyDied -= Add;
        }

        private void Add(Enemy enemy)
        {
            if (_enemyRewards.TryGetValue(enemy.Type, out int reward))
            {
                RewardCount += reward;
                OnCountChanged?.Invoke();
            }
        }
    }
}