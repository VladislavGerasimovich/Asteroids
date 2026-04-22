using System;
using Asteroids.Scripts.SaveSystem;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.SpawnTimers
{
    public class EnemySpawnTimer : ITickable
    {
        private SaveDataRepository _saveDataRepository;
        private float _accumulatedTime;

        public event Action OnSpawnTimeReached;

        public EnemySpawnTimer(SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
            _accumulatedTime = _saveDataRepository.SpawnDelay;
        }
        
        public void Tick()
        {
            CreateEnemies();
        }
        
        private void CreateEnemies()
        {
            if (_accumulatedTime >= _saveDataRepository.SpawnDelay)
            {
                OnSpawnTimeReached?.Invoke();
                _accumulatedTime = 0f;
            }
            
            _accumulatedTime += Time.deltaTime;
        }
    }
}