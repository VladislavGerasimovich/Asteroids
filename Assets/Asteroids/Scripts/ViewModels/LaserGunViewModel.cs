using System;
using Asteroids.Scripts.Guns;
using MVVM;
using UniRx;

namespace Asteroids.Scripts.ViewModels
{
    public class LaserGunViewModel : IDisposable
    {
        [Data("LaserGunBulletsInfo")]
        public ReactiveProperty<string> LaserGunBulletsInfo;
        
        private LaserGun _laserGun;

        public LaserGunViewModel()
        {
            LaserGunBulletsInfo = new ReactiveProperty<string>($"LaserGunBullets: 0/0");
        }

        public void Init(LaserGun laserGun)
        {
            _laserGun = laserGun;
            _laserGun.OnBulletSpent += OnLaserGunShoot;
            _laserGun.OnBulletAdded += OnLaserGunShoot;
            OnLaserGunShoot();
        }

        public void Dispose()
        {
            _laserGun.OnBulletSpent -= OnLaserGunShoot;
            _laserGun.OnBulletAdded -= OnLaserGunShoot;
        }

        private void OnLaserGunShoot()
        {
            LaserGunBulletsInfo.Value = $"LaserGunBullets: {_laserGun.CurrentAmmo}/{_laserGun.MaxAmmo}";
        }
    }
}