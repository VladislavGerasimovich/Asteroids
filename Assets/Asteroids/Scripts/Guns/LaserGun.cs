using System;
using Asteroids.Scripts.Bullets;

namespace Asteroids.Scripts.Guns
{
    public class LaserGun : IGun
    {
        public int MaxAmmo { get; private set; }
        public int CurrentAmmo { get; private set; }
        
        public event Action<Bullet> OnShoot;
        public event Action OnBulletSpent;
        public event Action OnBulletAdded;
        
        public bool CanShoot() => CurrentAmmo > 0;
        
        public LaserGun(int maxAmmo)
        {
            MaxAmmo = maxAmmo;
            CurrentAmmo = MaxAmmo;
        }

        public void Shoot()
        {
            CurrentAmmo--;
            OnShoot?.Invoke(new LaserGunBullet());
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