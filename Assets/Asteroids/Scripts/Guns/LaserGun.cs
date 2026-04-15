using System;
using Asteroids.Scripts.Bullets;

namespace Asteroids.Scripts.Guns
{
    public class LaserGun : IGun
    {
        private float _bulletLifeTime;
        private float _speed;
        
        public event Action<Bullet> OnShoot;
        public event Action OnBulletSpent;
        public event Action OnBulletAdded;
        
        public bool CanShoot() => CurrentAmmo > 0;
        public int MaxAmmo { get; private set; }
        public int CurrentAmmo { get; private set; }
        
        public LaserGun(int maxAmmo, float bulletLifeTime, float speed)
        {
            MaxAmmo = maxAmmo;
            CurrentAmmo = MaxAmmo;
            _bulletLifeTime = bulletLifeTime;
            _speed = speed;
        }

        public void Shoot()
        {
            CurrentAmmo--;
            OnShoot?.Invoke(new LaserGunBullet(_bulletLifeTime, _speed));
            OnBulletSpent?.Invoke();
        }

        public void TryAddBullet()
        {
            if (CurrentAmmo < MaxAmmo)
            {
                CurrentAmmo++;
                OnBulletAdded?.Invoke();
            }
        }
    }
}