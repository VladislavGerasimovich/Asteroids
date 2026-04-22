using UnityEngine;

namespace Asteroids.Scripts.SaveSystem
{
    public class SaveDataRepository
    {
        private SaveData _saveData;

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
        
        public SaveDataRepository(SaveBootstrapper saveBootstrapper)
        {
            _saveData = saveBootstrapper.Data;
        }
    }
}