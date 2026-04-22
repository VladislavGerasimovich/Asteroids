using System;
using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.PlayerShipMovement;
using Asteroids.Scripts.SaveSystem;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.OwnPhysics
{
    public class EnemyCollisionWithPlayer : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;
        private Vector2 _pushDirection;
        private SaveDataRepository _saveDataRepository;

        public EnemyCollisionWithPlayer(
            CollisionsRecords collisionsRecords,
            SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
            _collisionsRecords = collisionsRecords;
        }

        public void Initialize()
        {
            _collisionsRecords.OnPlayerEnemyCollision += OnCollisionWithPlayer;
        }

        public void Dispose()
        {
            _collisionsRecords.OnPlayerEnemyCollision -= OnCollisionWithPlayer;
        }
        
        private void OnCollisionWithPlayer(ShipMovement shipMovement, Enemy enemy)
        {
            _pushDirection = (enemy.Position - shipMovement.Position).normalized;
            enemy.ChangeMovement(_pushDirection, _saveDataRepository.BouncingTime);
        }
    }
}