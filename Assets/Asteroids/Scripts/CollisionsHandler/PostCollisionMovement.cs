using System;
using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.OwnPhysics;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.PlayerShip
{
    public class PostCollisionMovement : IInitializable, IDisposable
    {
        private CollisionsRecords _collisionsRecords;
        
        public Vector2 PushDirection { get; private set; }

        public PostCollisionMovement(CollisionsRecords collisionsRecords)
        {
            _collisionsRecords = collisionsRecords;
        }
        
        public void Initialize()
        {
            _collisionsRecords.OnPlayerEnemyCollision += Calculate;
        }

        public void Dispose()
        {
            _collisionsRecords.OnPlayerEnemyCollision -= Calculate;
        }

        private void Calculate(ShipMovement shipMovement, Enemy enemy)
        {
            PushDirection = (shipMovement.Position - enemy.Position).normalized;
        }
    }
}