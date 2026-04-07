using System;
using System.Collections.Generic;
using Asteroids.Scripts.Bullets;
using Asteroids.Scripts.Enemies;

namespace Asteroids.Scripts.OwnPhysics
{
    public class CollisionsRecords
    {
        public event Action<Enemy> OnEnemyDied;
        public event Action<DefaultBullet> DefaultBulletHitEnemy;
        public event Action GameEnd;
        
        public IEnumerable<Record> Values()
        {
            yield return IfCollided((Bullet bullet, Enemy enemy) =>
            {
                OnEnemyDied?.Invoke(enemy);
            });

            yield return IfCollided((DefaultBullet bullet, Enemy enemy) =>
            {
                DefaultBulletHitEnemy?.Invoke(bullet);
            });
            
            /*
            yield return IfCollided((Bullet bullet, Asteroid asteroid) =>
            {
                if (asteroid is PartOfAsteroid)
                    return;

                _enemies.Simulate(asteroid.CreatePart());
                _enemies.Simulate(asteroid.CreatePart());
                _enemies.Simulate(asteroid.CreatePart());
                _enemies.Simulate(asteroid.CreatePart());
            });

            yield return IfCollided((Ship ship, Enemy enemy) =>
            {
                GameEnd?.Invoke();
            });
            */
        }

        private Record IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}