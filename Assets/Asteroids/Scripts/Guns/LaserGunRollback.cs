namespace Asteroids.Scripts.Guns
{
    public class LaserGunRollback
    {
        private readonly float _reloadTime;
        private readonly LaserGun _laserGun;

        private float _accumulatedTime;

        public LaserGunRollback(LaserGun laserGun, float reloadTime = 3f)
        {
            _laserGun = laserGun;
            _reloadTime = reloadTime;
        }

        public void Update(float deltaTime)
        {
            if (_laserGun.CurrentAmmo < _laserGun.MaxAmmo)
            {
                _accumulatedTime += deltaTime;
                
                if (_accumulatedTime >= _reloadTime)
                {
                    _accumulatedTime = 0f;
                    _laserGun.TryAddBullet();
                }
            }
        }
    }
}