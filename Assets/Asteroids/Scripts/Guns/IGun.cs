using System;
using Asteroids.Scripts.Bullets;

namespace Asteroids.Scripts.Guns
{
    public interface IGun
    {
        event Action<IBullet> OnShoot;
        bool CanShoot();
        void Shoot();
    }
}