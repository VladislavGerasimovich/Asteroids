using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Scripts.Timers;
using UnityEngine;

namespace Asteroids.Scripts.Bullets
{
    public class BulletsSimulation
    {
        private readonly Timers<BulletEntity> _timers = new Timers<BulletEntity>();

        private List<BulletEntity> _entities = new List<BulletEntity>();

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

        /*
        public void StopAll(Bullet bullet)
        {
            List<BulletEntity> candidats = _entities.Where(entity => entity.Entity.Equals(bullet)).ToList();
            candidats.ForEach(Stop);
        }
        */
        private void Stop(BulletEntity bulletEntity)
        {
            _entities.Remove(bulletEntity);
            End?.Invoke(bulletEntity);
        }
    }

    public class BulletEntity
    {
        public readonly Bullet Entity;
        public readonly Trajectory BulletTrajectory;

        public BulletEntity(Bullet entity, Trajectory trajectory)
        {
            Entity = entity;
            BulletTrajectory = trajectory;
        }
    }
}