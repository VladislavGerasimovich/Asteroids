using System.Collections.Generic;
using System.Linq;
using Asteroids.Scripts.Enemies.Views;
using Asteroids.Scripts.PlayerShip;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Enemies
{
    public class EnemiesSpawner : ITickable
    {
        private EnemiesViewFactory _enemiesViewFactory;
        private Camera _camera;
        private ShipMovement _playerShipMovement;
        private List<Enemy> _enemies;
        private List<EnemyView> _enemyViews;

        private Enemy _enemyToRemove;
        private EnemyView _enemyViewToRemove;
        
        public EnemiesSpawner(Camera camera, EnemiesViewFactory enemiesViewFactory, ShipMovement playerShipMovement)
        {
            _camera = camera;
            _enemiesViewFactory = enemiesViewFactory;
            _playerShipMovement = playerShipMovement;
            _enemies = new List<Enemy>();
            _enemyViews = new List<EnemyView>();
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Nlo nlo = CreateNlo();
                nlo.OnEnded += Reset;
                CreateView(nlo);
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                Asteroid asteroid = CreateAsteroid();
                asteroid.OnEnded += Reset;
                CreateView(asteroid);
            }

            foreach (Enemy enemy in _enemies.ToList())
            {
                enemy.Update(Time.deltaTime);
            }
        }

        private void CreateView(Enemy enemy)
        {
            EnemyView nloView = _enemiesViewFactory.GetTemplate(enemy);
            nloView.Init(_camera, enemy);
            nloView.gameObject.SetActive(true);
            _enemies.Add(enemy);
            _enemyViews.Add(nloView);
        }

        private Nlo CreateNlo()
        {
            return new Nlo(GetRandomPositionInsideUnitCircle(), 0, 0.5f, _playerShipMovement);
        }
        
        private Asteroid CreateAsteroid()
        {
            Vector2 randomPosition = GetRandomPositionInsideUnitCircle();
            return new Asteroid(randomPosition, 0, GetDirectionThroughtScreen(randomPosition), 0.5f);
        }
        
        private Vector2 GetRandomPositionInsideUnitCircle()
        {
            return Random.insideUnitCircle.normalized + new Vector2(0.5f, 0.5f);
        }
        
        private static Vector2 GetDirectionThroughtScreen(Vector2 position)
        {
            return (new Vector2(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f)) - position).normalized;
        }

        private void Reset(Enemy enemy)
        {
            foreach (var entity in _enemies.ToList())
            {
                if (entity == enemy)
                {
                    _enemyToRemove = enemy;
                    break;
                }
            }

            foreach (EnemyView enemyView in _enemyViews)
            {
                if (enemyView.Enemy == enemy)
                {
                    _enemyViewToRemove = enemyView;
                    break;
                }
            }

            if (_enemyToRemove != null)
            {
                _enemies.Remove(_enemyToRemove);
            }
            
            if (_enemyViewToRemove != null)
            {
                _enemyViews.Remove(_enemyViewToRemove);
                
                _enemiesViewFactory.Reset(_enemyViewToRemove);
            }
        }
    }
}