using System;
using Asteroids.Scripts.Bullets;

namespace Asteroids.Scripts.Guns
{
    public class DefaultGun : IGun
    {
        private float _lifeTime;
        private float _speed;

        public DefaultGun(float lifeTime, float speed)
        {
            _speed = speed;
            _lifeTime = lifeTime;
        }
        
        public event Action<Bullet> OnShoot;
        
        public bool CanShoot() => true;

        public void Shoot()
        {
            OnShoot?.Invoke(new DefaultBullet(_lifeTime, _speed));
        }
    }
}