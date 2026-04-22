using System;
using Asteroids.Scripts.Bullets;
using Asteroids.Scripts.Guns;
using Asteroids.Scripts.SaveSystem;
using Asteroids.Scripts.ViewModels;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.PlayerShipWeaponsHandler
{
    public class WeaponsSystem : IInitializable, ITickable, IDisposable
    {
        private DefaultGun _defaultGun;
        private LaserGun _laserGun;
        private IGun _firstSlotGun;
        private IGun _secondSlotGun;
        private LaserGunRollback _laserGunRollback;
        private LaserGunViewModel _laserGunViewModel;
        private LaserGunRollbackViewModel _laserGunRollbackViewModel;
        private SaveDataRepository _saveDataRepository;

        public event Action<Bullet> OnGunShootEvent;

        public WeaponsSystem(
            LaserGunViewModel laserGunViewModel,
            LaserGunRollbackViewModel laserGunRollbackViewModel,
            SaveDataRepository saveDataRepository)
        {
            _saveDataRepository = saveDataRepository;
            _laserGunRollbackViewModel = laserGunRollbackViewModel;
            _laserGunViewModel = laserGunViewModel;
        }
        
        public void Initialize()
        {
            _defaultGun = new DefaultGun(_saveDataRepository.DefaultGunBulletLifeTime, _saveDataRepository.DefaultGunBulletSpeed);
            _laserGun = new LaserGun(_saveDataRepository.LaserGunBullets, _saveDataRepository.LaserGunBulletLifeTime, _saveDataRepository.LaserGunBulletSpeed);
            _laserGunViewModel.Init(_laserGun);
            _laserGunRollback = new LaserGunRollback(_laserGun, _saveDataRepository.LaserGunRollback);
            _laserGunRollbackViewModel.Init(_laserGunRollback);
            _firstSlotGun = _defaultGun;
            _secondSlotGun = _laserGun;

            _firstSlotGun.OnShoot += OnGunShoot;
            _secondSlotGun.OnShoot += OnGunShoot;
        }

        public void Tick()
        {
            _laserGunRollback.Update(Time.deltaTime);
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

        private void OnGunShoot(Bullet bullet)
        {
            OnGunShootEvent?.Invoke(bullet);
        }
    }
}