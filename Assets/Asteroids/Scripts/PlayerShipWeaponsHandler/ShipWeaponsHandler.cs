using System;
using System.Collections.Generic;
using Asteroids.Scripts.Bullets;
using Asteroids.Scripts.Guns;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.SaveSystem;
using Asteroids.Scripts.ViewFactories.Bullets;
using Asteroids.Scripts.ViewModels;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.PlayerShip
{
    public class ShipWeaponsHandler : IInitializable, ITickable, IDisposable
    {
        private readonly Dictionary<BulletEntity, BulletView> _views;
        private DefaultGun _defaultGun;
        private LaserGun _laserGun;
        private IGun _firstSlotGun;
        private IGun _secondSlotGun;
        private BulletsViewFactory _bulletsViewFactory;
        private BulletsSimulation _bulletsSimulation;
        private ShipMovement _shipMovement;
        private LaserGunRollback _laserGunRollback;
        private PhysicsRouter _physicsRouter;
        private CollisionsRecords _collisionsRecords;
        private LaserGunViewModel _laserGunViewModel;
        private LaserGunRollbackViewModel _laserGunRollbackViewModel;
        private DataManager _dataManager;

        public ShipWeaponsHandler(
            ShipMovement shipMovement,
            BulletsViewFactory bulletsViewFactory,
            PhysicsRouter physicsRouter,
            CollisionsRecords collisionsRecords,
            LaserGunViewModel laserGunViewModel,
            LaserGunRollbackViewModel laserGunRollbackViewModel,
            DataManager dataManager)
        {
            _dataManager = dataManager;
            _laserGunRollbackViewModel = laserGunRollbackViewModel;
            _laserGunViewModel = laserGunViewModel;
            _bulletsViewFactory = bulletsViewFactory;
            _shipMovement = shipMovement;
            _physicsRouter = physicsRouter;
            _collisionsRecords = collisionsRecords;
            _views = new Dictionary<BulletEntity, BulletView>();
        }
        
        public void Initialize()
        {
            _dataManager.LoadProgressOrInitNew();
            _bulletsSimulation = new BulletsSimulation();
            _defaultGun = new DefaultGun(_dataManager.DefaultGunBulletLifeTime, _dataManager.DefaultGunBulletSpeed);
            _laserGun = new LaserGun(_dataManager.LaserGunBullets, _dataManager.LaserGunBulletLifeTime, _dataManager.LaserGunBulletSpeed);
            _laserGunViewModel.Init(_laserGun);
            _laserGunRollback = new LaserGunRollback(_laserGun, _dataManager.LaserGunRollback);
            _laserGunRollbackViewModel.Init(_laserGunRollback);
            _firstSlotGun = _defaultGun;
            _secondSlotGun = _laserGun;

            _firstSlotGun.OnShoot += OnGunShoot;
            _secondSlotGun.OnShoot += OnGunShoot;
            _bulletsSimulation.Start += OnStartBulletSimulation;
            _bulletsSimulation.End += OnEndBulletSimulation;
            _collisionsRecords.OnDefaultBulletHitEnemy += OnDefaultBulletDied;
        }

        public void Tick()
        {
            _bulletsSimulation.Update(Time.deltaTime);
            _laserGunRollback.Update(Time.deltaTime);
        }

        public void Dispose()
        {
            _firstSlotGun.OnShoot -= OnGunShoot;
            _secondSlotGun.OnShoot -= OnGunShoot;
            _bulletsSimulation.Start -= OnStartBulletSimulation;
            _bulletsSimulation.End -= OnEndBulletSimulation;
            _collisionsRecords.OnDefaultBulletHitEnemy -= OnDefaultBulletDied;
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
            _bulletsSimulation.Simulate(bullet, _shipMovement.Position, _shipMovement.Forward);
        }

        private void OnStartBulletSimulation(BulletEntity bulletEntity)
        {
            BulletView bulletView = _bulletsViewFactory.GetTemplate(bulletEntity.Entity);
            bulletView.Init(bulletEntity.BulletTrajectory);
            
            PhysicsEventsBroadcaster physicsEventsBroadcaster = bulletView.GetComponent<PhysicsEventsBroadcaster>();
            physicsEventsBroadcaster.Init(_physicsRouter, bulletEntity.Entity);
            
            bulletView.gameObject.SetActive(true);
            
            _views.Add(bulletEntity, bulletView);
        }

        private void OnEndBulletSimulation(BulletEntity bulletEntity)
        {
            BulletView bulletView = _views[bulletEntity];
            _bulletsViewFactory.Reset(bulletEntity, bulletView);
        }

        private void OnDefaultBulletDied(Bullet bullet)
        {
            foreach (BulletEntity bulletEntity in _views.Keys)
            {
                if (bulletEntity.Entity == bullet)
                {
                    BulletView bulletView = _views[bulletEntity];
                    _bulletsViewFactory.Reset(bulletEntity, bulletView);

                    break;
                }
            }
        }
    }
}