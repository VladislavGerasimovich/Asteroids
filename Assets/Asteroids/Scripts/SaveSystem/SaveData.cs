namespace Asteroids.Scripts.SaveSystem
{
    public class SaveData
    {
        public PlayerShipData PlayerShip;
        public PlayerWeaponData PlayerWeapon;
        public EnemiesData Enemies;

        public SaveData()
        {
            PlayerShip = new PlayerShipData();
            PlayerWeapon = new PlayerWeaponData();
            Enemies = new EnemiesData();
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
}