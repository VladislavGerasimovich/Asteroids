namespace Asteroids.Scripts.Guns
{
    public class LaserGunRollback
    {
        private readonly float _reloadTime;
        private readonly LaserGun _laserGun;

        public float AccumulatedTime { get; private set; }

        public LaserGunRollback(LaserGun laserGun, float reloadTime = 3f)
        {
            _laserGun = laserGun;
            _reloadTime = reloadTime;
            AccumulatedTime = _reloadTime;
        }

        public void Update(float deltaTime)
        {
            if (_laserGun.CurrentAmmo < _laserGun.MaxAmmo)
            {
                AccumulatedTime -= deltaTime;
                
                if (AccumulatedTime <= 0)
                {
                    _laserGun.TryAddBullet();
                    AccumulatedTime = _reloadTime;
                }
            }
        }
    }
}