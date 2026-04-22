using System;
using System.Collections.Generic;
using Asteroids.Scripts.Timers;
using UnityEngine;

namespace Asteroids.Scripts.Bullets
{
    public class BulletsSimulation
    {
        private readonly Timers<BulletEntity> _timers = new Timers<BulletEntity>();
        private readonly List<BulletEntity> _entities = new List<BulletEntity>();

        public event Action<BulletEntity> Start;
        public event Action<BulletEntity> End;

        public void Simulate(Bullet bullet, Vector2 startPosition, Vector2 direction)
        {
            BulletEntity bulletEntity = null;
            Trajectory trajectory = new Trajectory(bullet.Speed, startPosition, direction,
                (_) => _timers.GetAccumulatedTime(bulletEntity));
            bulletEntity = new BulletEntity(bullet, trajectory);
            _timers.Start(bulletEntity, bullet.Lifetime, Stop);
            _entities.Add(bulletEntity);
            Start?.Invoke(bulletEntity);
        }

        public void Update(float deltaTime)
        {
            _timers.Tick(deltaTime);
        }

        private void Stop(BulletEntity bulletEntity)
        {
            _entities.Remove(bulletEntity);
            End?.Invoke(bulletEntity);
        }
    }
}