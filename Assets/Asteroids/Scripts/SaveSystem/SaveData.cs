namespace Asteroids.Scripts.SaveSystem
{
    public class SaveData
    {
        public PlayerShipData PlayerShip;
        public PlayerWeaponData PlayerWeapon;
        public EnemiesData Enemies;
        public ScreenResolutionSetter ResolutionSetter;
        public EnemiesSpawnerData EnemiesSpawner;
        public RewardsData RewardsData;
        
        public SaveData()
        {
            PlayerShip = new PlayerShipData();
            PlayerWeapon = new PlayerWeaponData();
            Enemies = new EnemiesData();
            ResolutionSetter = new ScreenResolutionSetter();
            EnemiesSpawner = new EnemiesSpawnerData();
            RewardsData = new RewardsData();
        }
    }

    public class PlayerShipData
    {
        public float X;
        public float Y;
        public float Rotation;
        public float UnitsPerSecond;
        public float MaxSpeed;
        public float MaxBounceSpeed;
        public float SecondsToStop;
        public float Health;
    }

    public class PlayerWeaponData
    {
        public int LaserGunBullets;
        public float LaserGunRollback;
        public float LaserGunBulletLifeTime;
        public float LaserGunBulletSpeed;

        public float DefaultGunBulletLifeTime;
        public float DefaultGunBulletSpeed;
    }

    public class EnemiesData
    {
        public float UfoSpeed;
        public float AsteroidSpeed;
        public float PartOfAsteroidSpeed;
        public float BouncingTime;
    }

    public class EnemiesSpawnerData
    {
        public int MaxEnemyCount;
        public float SpawnDelay;
    }

    public class ScreenResolutionSetter
    {
        public int Width;
        public int Height;
        public bool IsFullScreen;
    }

    public class RewardsData
    {
        public int UfoReward;
        public int AsteroidReward;
        public int PartOfAsteroidReward;
    }
}