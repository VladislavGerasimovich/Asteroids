using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShip;
using Asteroids.Scripts.ViewFactories.Enemies;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.Enemies
{
    public class EnemiesSpawner : IInitializable, ITickable, IDisposable
    {
        private EnemiesViewFactory _enemiesViewFactory;
        private Camera _camera;
        private ShipMovement _playerShipMovement;
        private List<Enemy> _enemies;
        private List<EnemyView> _enemyViews;
        private PhysicsRouter _physicsRouter;
        private CollisionsRecords _collisionsRecords;
        private int _minAsteroidPartsCount;
        private int _maxAsteroidPartsCount;
        private int _asteroidPartsCount;
        
        private Enemy _enemyToRemove;
        private EnemyView _enemyViewToRemove;
        
        public EnemiesSpawner(
            Camera camera,
            EnemiesViewFactory enemiesViewFactory,
            ShipMovement playerShipMovement,
            PhysicsRouter physicsRouter,
            CollisionsRecords collisionsRecords)
        {
            _camera = camera;
            _enemiesViewFactory = enemiesViewFactory;
            _playerShipMovement = playerShipMovement;
            _physicsRouter = physicsRouter;
            _collisionsRecords = collisionsRecords;
            _enemies = new List<Enemy>();
            _enemyViews = new List<EnemyView>();
            _minAsteroidPartsCount = 1;
            _maxAsteroidPartsCount = 4;
        }

        public void Initialize()
        {
            _collisionsRecords.OnEnemyDied += Reset;
            _collisionsRecords.OnAsteroidDestroyed += CreateAsteroidParts;
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

        public void Dispose()
        {
            _collisionsRecords.OnEnemyDied -= Reset;
            _collisionsRecords.OnAsteroidDestroyed -= CreateAsteroidParts;
        }

        private void CreateView(Enemy enemy)
        {
            EnemyView enemyView = _enemiesViewFactory.GetTemplate(enemy);
            enemyView.Init(_camera, enemy);
            
            PhysicsEventsBroadcaster physicsEventsBroadcaster = enemyView.GetComponent<PhysicsEventsBroadcaster>();
            physicsEventsBroadcaster.Init(_physicsRouter, enemy);
            
            if (enemyView.TryGetComponent(out BoxCollider2D boxCollider))
                boxCollider.enabled = true;
            
            enemyView.gameObject.SetActive(true);
            _enemies.Add(enemy);
            _enemyViews.Add(enemyView);
        }

        private Nlo CreateNlo()
        {
            return new Nlo(GetRandomPositionInsideUnitCircle(), 0, 0.1f, _playerShipMovement);
        }
        
        private Asteroid CreateAsteroid()
        {
            Vector2 randomPosition = GetRandomPositionInsideUnitCircle();
            return new Asteroid(randomPosition, 0, GetDirectionThroughtScreen(randomPosition), 0.1f);
        }
        
        private PartOfAsteroid CreatePartOfAsteroid(Asteroid asteroid)
        {
            return new PartOfAsteroid(asteroid.Position, 0, GetDirectionThroughtScreen(asteroid.Position), 0.1f);
        }
        
        private Vector2 GetRandomPositionInsideUnitCircle()
        {
            return Random.insideUnitCircle.normalized + new Vector2(0.5f, 0.5f);
        }
        
        private static Vector2 GetDirectionThroughtScreen(Vector2 position)
        {
            return (new Vector2(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f)) - position).normalized;
        }

        private void CreateAsteroidParts(Asteroid asteroid)
        {
            _asteroidPartsCount = Random.Range(_minAsteroidPartsCount, _maxAsteroidPartsCount);

            for (int i = 0; i < _asteroidPartsCount; i++)
            {
                PartOfAsteroid partOfAsteroid = CreatePartOfAsteroid(asteroid);
                partOfAsteroid.OnEnded += Reset;
                CreateView(partOfAsteroid);
            }
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
                if (_enemyViewToRemove.TryGetComponent(out BoxCollider2D boxCollider))
                    boxCollider.enabled = false;
                
                _enemyViews.Remove(_enemyViewToRemove);
                
                _enemiesViewFactory.Reset(_enemyViewToRemove);
            }
        }
    }
}