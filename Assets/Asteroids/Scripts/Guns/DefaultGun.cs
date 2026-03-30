using System;
using Asteroids.Scripts.Bullets;

namespace Asteroids.Scripts.Guns
{
    public class DefaultGun : IGun
    {
        public event Action<IBullet> OnShoot;
        
        public bool CanShoot() => true;

        public void Shoot()
        {
            OnShoot?.Invoke(new DefaultBullet());
        }
    }
}