using System;
using System.Collections.Generic;
using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShipMovement;
using Asteroids.Scripts.SaveSystem;
using Asteroids.Scripts.SpawnTimers;
using Asteroids.Scripts.Utils;
using Asteroids.Scripts.ViewFactories.Enemies;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.Spawners
{
    public class EnemiesSpawner : IInitializable, ITickable, IDisposable
    {
        private EnemiesViewFactory _enemiesViewFactory;
        private Camera _camera;
        private ShipMovement _playerShipMovement;
        private List<Enemy> _enemies;
        private List<Enemy> _enemiesBuffer;
        private List<EnemyView> _enemyViews;
        private CollisionsRecords _collisionsRecords;
        private Enemy _enemyToRemove;
        private EnemyView _enemyViewToRemove;
        private SaveDataRepository _saveDataRepository;
        private int _minAsteroidPartsCount;
        private int _maxAsteroidPartsCount;
        private int _asteroidPartsCount;
        private int _randomEnemyIndex;
        private PositionUtils _positionUtils;
        private EnemySpawnTimer _enemySpawnTimer;
        private EnemyViewCreator _enemyViewCreator;
        private EnemyView _tempEnemyView;

        public EnemiesSpawner(
            Camera camera,
            EnemiesViewFactory enemiesViewFactory,
            ShipMovement playerShipMovement,
            CollisionsRecords collisionsRecords,
            SaveDataRepository saveDataRepository,
            PositionUtils positionUtils,
            EnemySpawnTimer enemySpawnTimer,
            EnemyViewCreator enemyViewCreator)
        {
            _enemyViewCreator = enemyViewCreator;
            _enemySpawnTimer = enemySpawnTimer;
            _positionUtils = positionUtils;
            _saveDataRepository = saveDataRepository;
            _camera = camera;
            _enemiesViewFactory = enemiesViewFactory;
            _playerShipMovement = playerShipMovement;
            _collisionsRecords = collisionsRecords;
            _enemies = new List<Enemy>();
            _enemiesBuffer = new List<Enemy>();
            _enemyViews = new List<EnemyView>();
            _minAsteroidPartsCount = 1;
            _maxAsteroidPartsCount = 4;
        }

        public void Initialize()
        {
            _collisionsRecords.OnEnemyDied += Reset;
            _collisionsRecords.OnAsteroidDestroyed += CreateAsteroidParts;
            _enemySpawnTimer.OnSpawnTimeReached += CreateRandomEnemy;
        }

        public void Tick()
        {
            _enemiesBuffer.Clear();
            _enemiesBuffer.AddRange(_enemies);

            foreach (Enemy enemy in _enemiesBuffer)
            {
                enemy.Update(Time.deltaTime);
            }
        }

        public void Dispose()
        {
            _collisionsRecords.OnEnemyDied -= Reset;
            _collisionsRecords.OnAsteroidDestroyed -= CreateAsteroidParts;
            _enemySpawnTimer.OnSpawnTimeReached -= CreateRandomEnemy;
        }

        private void CreateRandomEnemy()
        {
            if (_enemies.Count < _saveDataRepository.MaxEnemyCount)
            {
                _randomEnemyIndex = Random.Range(0, 2);

                if (_randomEnemyIndex == 0)
                {
                    Ufo ufo = CreateUfo();
                    ufo.OnEnded += Reset;
                    _tempEnemyView = _enemyViewCreator.CreateView(ufo);
                    _enemies.Add(ufo);
                    _enemyViews.Add(_tempEnemyView);
                }
                else if (_randomEnemyIndex == 1)
                {
                    Asteroid asteroid = CreateAsteroid();
                    asteroid.OnEnded += Reset;
                    _tempEnemyView = _enemyViewCreator.CreateView(asteroid);
                    _enemies.Add(asteroid);
                    _enemyViews.Add(_tempEnemyView);
                }
            }
        }

        private Ufo CreateUfo()
        {
            return new Ufo(_positionUtils.GetRandomPositionInsideUnitCircle(), 0, _saveDataRepository.UfoSpeed,
                _playerShipMovement);
        }

        private Asteroid CreateAsteroid()
        {
            Vector2 randomPosition = _positionUtils.GetRandomPositionInsideUnitCircle();
            return new Asteroid(randomPosition, 0, _positionUtils.GetDirectionThroughScreen(randomPosition),
                _saveDataRepository.AsteroidSpeed);
        }

        private PartOfAsteroid CreatePartOfAsteroid(Asteroid asteroid)
        {
            return new PartOfAsteroid(asteroid.Position, 0, _positionUtils.GetDirectionThroughScreen(asteroid.Position),
                _saveDataRepository.PartOfAsteroidSpeed);
        }

        private void CreateAsteroidParts(Asteroid asteroid)
        {
            _asteroidPartsCount = Random.Range(_minAsteroidPartsCount, _maxAsteroidPartsCount);

            for (int i = 0; i < _asteroidPartsCount; i++)
            {
                PartOfAsteroid partOfAsteroid = CreatePartOfAsteroid(asteroid);
                partOfAsteroid.OnEnded += Reset;
                _tempEnemyView = _enemyViewCreator.CreateView(partOfAsteroid);
                _enemies.Add(partOfAsteroid);
                _enemyViews.Add(_tempEnemyView);
            }
        }

        private void Reset(Enemy enemy)
        {
            foreach (var entity in _enemies)
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
                _enemyToRemove.OnEnded -= Reset;
            }

            if (_enemyViewToRemove != null)
            {
                if (_enemyViewToRemove.TryGetComponent(out BoxCollider2D boxCollider))
                    boxCollider.enabled = false;

                _enemyViews.Remove(_enemyViewToRemove);
                _enemyViewToRemove.transform.position =
                    _camera.ViewportToWorldPoint(_positionUtils.GetRandomPositionInsideUnitCircle());
                _enemiesViewFactory.Reset(_enemyViewToRemove);
            }
        }
    }
}