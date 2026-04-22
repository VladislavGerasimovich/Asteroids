namespace Asteroids.Scripts.SaveSystem
{
    public class GameBalanceConfig
    {
        public void InitNewData(SaveData saveData)
        {
            saveData.PlayerShip.X = 0.5f;
            saveData.PlayerShip.Y = 0.5f;
            saveData.PlayerShip.Rotation = 0;
            saveData.PlayerShip.UnitsPerSecond = 0.0005f;
            saveData.PlayerShip.MaxSpeed = 0.0015f;
            saveData.PlayerShip.MaxBounceSpeed = 0.0035f;
            saveData.PlayerShip.SecondsToStop = 0.2f;
            saveData.PlayerShip.Health = 3;
            
            saveData.PlayerWeapon.LaserGunBullets = 10;
            saveData.PlayerWeapon.LaserGunRollback = 5;
            saveData.PlayerWeapon.LaserGunBulletLifeTime = 1.5f;
            saveData.PlayerWeapon.LaserGunBulletSpeed = 0f;
            
            saveData.PlayerWeapon.DefaultGunBulletLifeTime = 3f;
            saveData.PlayerWeapon.DefaultGunBulletSpeed = 2f;

            saveData.Enemies.UfoSpeed = 0.07f;
            saveData.Enemies.AsteroidSpeed = 0.1f;
            saveData.Enemies.PartOfAsteroidSpeed = 0.2f;
            saveData.Enemies.BouncingTime = 3f;

            saveData.EnemiesSpawner.MaxEnemyCount = 10;
            saveData.EnemiesSpawner.SpawnDelay = 1f;

            saveData.ResolutionSetter.Width = 1000;
            saveData.ResolutionSetter.Height = 1000;
            saveData.ResolutionSetter.IsFullScreen = true;

            saveData.RewardsData.UfoReward = 150;
            saveData.RewardsData.AsteroidReward = 100;
            saveData.RewardsData.PartOfAsteroidReward = 50;
        }
    }
}