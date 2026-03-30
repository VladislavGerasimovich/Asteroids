using System;
using Asteroids.Scripts.Bullets;
using Asteroids.Scripts.Bullets.Views;
using Asteroids.Scripts.Guns;
using Zenject;

namespace Asteroids.Scripts.PlayerShip
{
    public class ShipWeaponsHandler : IInitializable, IDisposable
    {
        private DefaultGun _defaultGun;
        private LaserGun _laserGun;
        private IGun _firstSlotGun;
        private IGun _secondSlotGun;
        private BulletsViewFactory _bulletsViewFactory;

        public ShipWeaponsHandler(BulletsViewFactory bulletsViewFactory)
        {
            _bulletsViewFactory = bulletsViewFactory;
        }
        
        public void Initialize()
        {
            _defaultGun = new DefaultGun();
            _laserGun = new LaserGun();
            _firstSlotGun = _defaultGun;
            _secondSlotGun = _laserGun;

            _firstSlotGun.OnShoot += OnGunShoot;
            _secondSlotGun.OnShoot += OnGunShoot;
        }

        public void Dispose()
        {
            _firstSlotGun.OnShoot -= OnGunShoot;
            _secondSlotGun.OnShoot -= OnGunShoot;
        }
        
        public void OnFirstSlotGunButtonClicked()
        {
            if(_firstSlotGun.CanShoot())
                _firstSlotGun.Shoot();
        }
        
        public void OnSecondSlotGunButtonClicked()
        {
            if(_secondSlotGun.CanShoot())
                _secondSlotGun.Shoot();
        }

        private void OnGunShoot(IBullet bullet)
        {
            BulletView bulletView = _bulletsViewFactory.GetTemplate(bullet);
            bulletView.gameObject.SetActive(true);
        }
    }
}