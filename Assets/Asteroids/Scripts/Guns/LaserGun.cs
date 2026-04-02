using System;
using Asteroids.Scripts.Bullets;

namespace Asteroids.Scripts.Guns
{
    public class LaserGun : IGun
    {
        public int MaxAmmo { get; private set; }
        public int CurrentAmmo { get; private set; }

        public LaserGun(int maxAmmo)
        {
            MaxAmmo = maxAmmo;
            CurrentAmmo = MaxAmmo;
        }
        
        public event Action<Bullet> OnShoot;
        
        public bool CanShoot() => CurrentAmmo > 0;

        public void Shoot()
        {
            OnShoot?.Invoke(new LaserGunBullet());
            CurrentAmmo--;
        }

        public void TryAddBullet()
        {
            if (CurrentAmmo < MaxAmmo)
            {
                CurrentAmmo++;
            }
        }
    }
}