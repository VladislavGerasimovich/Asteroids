using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.SaveSystem
{
    public class DataManager : ITickable
    {
        private SaveData _saveData;
        private JsonSaver _jsonSaver;
        private bool _hasSavedData;

        public Vector2 PlayerPosition => new Vector2(_saveData.PlayerShip.X, _saveData.PlayerShip.Y);
        public float PlayerRotation => _saveData.PlayerShip.Rotation;
        public float UnitsPerSecond => _saveData.PlayerShip.UnitsPerSecond;
        public float MaxSpeed => _saveData.PlayerShip.MaxSpeed;
        public float MaxBounceSpeed => _saveData.PlayerShip.MaxBounceSpeed;
        public float SecondsToStop => _saveData.PlayerShip.SecondsToStop;
        public float PlayerShipHealth => _saveData.PlayerShip.Health;
        
        public int LaserGunBullets => _saveData.PlayerWeapon.LaserGunBullets;
        public float LaserGunRollback => _saveData.PlayerWeapon.LaserGunRollback;
        public float LaserGunBulletLifeTime => _saveData.PlayerWeapon.LaserGunBulletLifeTime;
        public float LaserGunBulletSpeed => _saveData.PlayerWeapon.LaserGunBulletSpeed;
        
        public float DefaultGunBulletLifeTime => _saveData.PlayerWeapon.DefaultGunBulletLifeTime;
        public float DefaultGunBulletSpeed => _saveData.PlayerWeapon.DefaultGunBulletSpeed;

        public float UfoSpeed => _saveData.Enemies.UfoSpeed;
        public float AsteroidSpeed => _saveData.Enemies.AsteroidSpeed;
        public float PartOfAsteroidSpeed => _saveData.Enemies.PartOfAsteroidSpeed;
        public float BouncingTime => _saveData.Enemies.BouncingTime;

        public int MaxEnemyCount => _saveData.EnemiesSpawner.MaxEnemyCount;
        public float SpawnDelay => _saveData.EnemiesSpawner.SpawnDelay;

        public int ScreenWidth => _saveData.ResolutionSetter.Width;
        public int ScreenHeight => _saveData.ResolutionSetter.Height;
        public bool IsFullScreen => _saveData.ResolutionSetter.IsFullScreen;

        public int UfoReward => _saveData.RewardsData.UfoReward;
        public int AsteroidReward => _saveData.RewardsData.AsteroidReward;
        public int PartOfAsteroidReward => _saveData.RewardsData.PartOfAsteroidReward;
        
        public DataManager()
        {
            _saveData = new SaveData();
            _jsonSaver = new JsonSaver();
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                InitNewData();
                Save();
            }
        }
        
        public void LoadProgressOrInitNew()
        {
            _hasSavedData = _jsonSaver.Load(ref _saveData);
            
            if(_hasSavedData == false)
            {
                InitNewData();
                Save();
            }
        }

        private void Save()
        {
            _jsonSaver.Save(_saveData);
        }
        
        private void InitNewData()
        {
            _saveData.PlayerShip.X = 0.5f;
            _saveData.PlayerShip.Y = 0.5f;
            _saveData.PlayerShip.Rotation = 0;
            _saveData.PlayerShip.UnitsPerSecond = 0.0005f;
            _saveData.PlayerShip.MaxSpeed = 0.0015f;
            _saveData.PlayerShip.MaxBounceSpeed = 0.0035f;
            _saveData.PlayerShip.SecondsToStop = 0.2f;
            _saveData.PlayerShip.Health = 3;
            
            _saveData.PlayerWeapon.LaserGunBullets = 7;
            _saveData.PlayerWeapon.LaserGunRollback = 5;
            _saveData.PlayerWeapon.LaserGunBulletLifeTime = 1.5f;
            _saveData.PlayerWeapon.LaserGunBulletSpeed = 0f;
            
            _saveData.PlayerWeapon.DefaultGunBulletLifeTime = 3f;
            _saveData.PlayerWeapon.DefaultGunBulletSpeed = 2f;

            _saveData.Enemies.UfoSpeed = 0.07f;
            _saveData.Enemies.AsteroidSpeed = 0.1f;
            _saveData.Enemies.PartOfAsteroidSpeed = 0.2f;
            _saveData.Enemies.BouncingTime = 3f;

            _saveData.EnemiesSpawner.MaxEnemyCount = 10;
            _saveData.EnemiesSpawner.SpawnDelay = 1f;

            _saveData.ResolutionSetter.Width = 1000;
            _saveData.ResolutionSetter.Height = 1000;
            _saveData.ResolutionSetter.IsFullScreen = true;

            _saveData.RewardsData.UfoReward = 150;
            _saveData.RewardsData.AsteroidReward = 100;
            _saveData.RewardsData.PartOfAsteroidReward = 50;
        }
    }
}