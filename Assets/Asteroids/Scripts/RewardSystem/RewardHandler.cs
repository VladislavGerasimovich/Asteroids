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

        public int RewardCount { get; private set; }

        public event Action OnCountChanged;
        
        public RewardHandler(CollisionsRecords collisionsRecords, DataManager dataManager)
        {
            _collisionsRecords = collisionsRecords;
            dataManager.LoadProgressOrInitNew();
            
            _enemyRewards = new Dictionary<EnemyType, int>()
            {
                { EnemyType.Ufo, dataManager.UfoReward },
                { EnemyType.Asteroid, dataManager.AsteroidReward },
                { EnemyType.PartOfAsteroid, dataManager.PartOfAsteroidReward },
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