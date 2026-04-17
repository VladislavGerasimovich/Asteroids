using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShip;
using Asteroids.Scripts.SaveSystem;
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
        private Enemy _enemyToRemove;
        private EnemyView _enemyViewToRemove;
        private DataManager _dataManager;
        private Vector2 _pushDirection;
        private Vector2 _circlePositionOffset;
        private int _minAsteroidPartsCount;
        private int _maxAsteroidPartsCount;
        private int _asteroidPartsCount;
        private int _randomEnemyIndex;
        private float _positionMinOffset;
        private float _positionMaxOffset;
        private float _accumulatedTime;

        public EnemiesSpawner(
            Camera camera,
            EnemiesViewFactory enemiesViewFactory,
            ShipMovement playerShipMovement,
            PhysicsRouter physicsRouter,
            CollisionsRecords collisionsRecords,
            DataManager dataManager)
        {
            _dataManager = dataManager;
            _dataManager.LoadProgressOrInitNew();
            _camera = camera;
            _enemiesViewFactory = enemiesViewFactory;
            _playerShipMovement = playerShipMovement;
            _physicsRouter = physicsRouter;
            _collisionsRecords = collisionsRecords;
            _enemies = new List<Enemy>();
            _enemyViews = new List<EnemyView>();
            _minAsteroidPartsCount = 1;
            _maxAsteroidPartsCount = 4;
            _circlePositionOffset = new Vector2(0.5f, 0.5f);
            _positionMinOffset = 0.1f;
            _positionMaxOffset = 0.9f;
            _accumulatedTime = _dataManager.SpawnDelay;
        }

        public void Initialize()
        {
            _collisionsRecords.OnEnemyDied += Reset;
            _collisionsRecords.OnAsteroidDestroyed += CreateAsteroidParts;
            _collisionsRecords.OnPlayerEnemyCollision += OnCollisionWithPlayer;
        }

        public void Tick()
        {
            CreateEnemies();
            
            foreach (Enemy enemy in _enemies.ToList())
            {
                enemy.Update(Time.deltaTime);
            }
        }

        public void Dispose()
        {
            _collisionsRecords.OnEnemyDied -= Reset;
            _collisionsRecords.OnAsteroidDestroyed -= CreateAsteroidParts;
            _collisionsRecords.OnPlayerEnemyCollision -= OnCollisionWithPlayer;
        }

        private void CreateView(Enemy enemy)
        {
            EnemyView enemyView = _enemiesViewFactory.GetTemplate(enemy);
            enemyView.Init(_camera, enemy);
            
            PhysicsEventsBroadcaster physicsEventsBroadcaster = enemyView.GetComponent<PhysicsEventsBroadcaster>();
            physicsEventsBroadcaster.Init(_physicsRouter, enemy);
            
            if (enemyView.TryGetComponent(out BoxCollider2D boxCollider))
                boxCollider.enabled = true;

            enemyView.transform.position = _camera.ViewportToWorldPoint(GetRandomPositionInsideUnitCircle());
            enemyView.gameObject.SetActive(true);
            _enemies.Add(enemy);
            _enemyViews.Add(enemyView);
        }

        private void CreateEnemies()
        {
            if (_enemies.Count < _dataManager.MaxEnemyCount)
            {
                if (_accumulatedTime >= _dataManager.SpawnDelay)
                {
                    CreateRandomEnemy();
                    _accumulatedTime = 0f;
                }
                
                _accumulatedTime += Time.deltaTime;
            }
        }

        private void CreateRandomEnemy()
        {
            _randomEnemyIndex = Random.Range(0, 2);

            if (_randomEnemyIndex == 0)
            {
                Ufo ufo = CreateUfo();
                ufo.OnEnded += Reset;
                CreateView(ufo);
            }
            else if (_randomEnemyIndex == 1)
            {
                Asteroid asteroid = CreateAsteroid();
                asteroid.OnEnded += Reset;
                CreateView(asteroid);
            }
        }

        private Ufo CreateUfo()
        {
            return new Ufo(GetRandomPositionInsideUnitCircle(), 0, _dataManager.UfoSpeed, _playerShipMovement);
        }
        
        private Asteroid CreateAsteroid()
        {
            Vector2 randomPosition = GetRandomPositionInsideUnitCircle();
            return new Asteroid(randomPosition, 0, GetDirectionThroughtScreen(randomPosition), _dataManager.AsteroidSpeed);
        }
        
        private PartOfAsteroid CreatePartOfAsteroid(Asteroid asteroid)
        {
            return new PartOfAsteroid(asteroid.Position, 0, GetDirectionThroughtScreen(asteroid.Position), _dataManager.PartOfAsteroidSpeed);
        }
        
        private Vector2 GetRandomPositionInsideUnitCircle()
        {
            return Random.insideUnitCircle.normalized + _circlePositionOffset;
        }
        
        private Vector2 GetDirectionThroughtScreen(Vector2 position)
        {
            return (new Vector2(Random.Range(_positionMinOffset, _positionMaxOffset), Random.Range(_positionMinOffset, _positionMaxOffset)) - position).normalized;
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
                _enemyViewToRemove.transform.position = _camera.ViewportToWorldPoint(GetRandomPositionInsideUnitCircle());
                _enemiesViewFactory.Reset(_enemyViewToRemove);
            }
        }

        private void OnCollisionWithPlayer(ShipMovement shipMovement, Enemy enemy)
        {
            _pushDirection = (enemy.Position - shipMovement.Position).normalized;

            enemy.ChangeMovement(_pushDirection, _dataManager.BouncingTime);
        }
    }
}