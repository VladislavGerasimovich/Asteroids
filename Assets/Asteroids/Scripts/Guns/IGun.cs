using System;
using Asteroids.Scripts.Bullets;

namespace Asteroids.Scripts.Guns
{
    public interface IGun
    {
        event Action<Bullet> OnShoot;
        bool CanShoot();
        void Shoot();
    }
}