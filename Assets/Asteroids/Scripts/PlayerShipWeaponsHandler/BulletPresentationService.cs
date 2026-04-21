using System;
using System.Collections.Generic;
using Asteroids.Scripts.Bullets;
using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShipMovement;
using Asteroids.Scripts.ViewFactories.Bullets;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.PlayerShipWeaponsHandler
{
    public class BulletPresentationService : IInitializable, ITickable, IDisposable
    {
        private readonly Dictionary<BulletEntity, BulletView> _views;
        private readonly Dictionary<Bullet, BulletEntity> _bulletEntities;
        
        private BulletsSimulation _bulletsSimulation;
        private BulletsViewFactory _bulletsViewFactory;
        private PhysicsRouter _physicsRouter;
        private CollisionsRecords _collisionsRecords;
        private WeaponsSystem _weaponsSystem;
        private ShipMovement _shipMovement;
        private Camera _camera;

        public BulletPresentationService(
            ShipMovement shipMovement,
            BulletsViewFactory bulletsViewFactory,
            PhysicsRouter physicsRouter,
            CollisionsRecords collisionsRecords,
            WeaponsSystem weaponsSystem,
            Camera camera)
        {
            _camera = camera;
            _weaponsSystem = weaponsSystem;
            _collisionsRecords = collisionsRecords;
            _physicsRouter = physicsRouter;
            _bulletsViewFactory = bulletsViewFactory;
            _shipMovement = shipMovement;
            _views = new Dictionary<BulletEntity, BulletView>();
            _bulletEntities = new Dictionary<Bullet, BulletEntity>();
        }

        public void Initialize()
        {
            _bulletsSimulation = new BulletsSimulation();
            _bulletsSimulation.Start += OnStartBulletSimulation;
            _bulletsSimulation.End += OnEndBulletSimulation;
            _collisionsRecords.OnDefaultBulletHitEnemy += OnDefaultBulletDied;
            _weaponsSystem.OnGunShootEvent += CreateBullet;
        }

        public void Tick()
        {
            _bulletsSimulation.Update(Time.deltaTime);
        }
        
        public void Dispose()
        {
            _bulletsSimulation.Start -= OnStartBulletSimulation;
            _bulletsSimulation.End -= OnEndBulletSimulation;
            _collisionsRecords.OnDefaultBulletHitEnemy -= OnDefaultBulletDied;
            _weaponsSystem.OnGunShootEvent -= CreateBullet;
        }
        
        private void OnStartBulletSimulation(BulletEntity bulletEntity)
        {
            BulletView bulletView = _bulletsViewFactory.GetTemplate(bulletEntity.Entity);
            bulletView.Init(bulletEntity.BulletTrajectory, _camera);
            
            PhysicsEventsBroadcaster physicsEventsBroadcaster = bulletView.GetComponent<PhysicsEventsBroadcaster>();
            physicsEventsBroadcaster.Init(_physicsRouter, bulletEntity.Entity);
            
            bulletView.gameObject.SetActive(true);
            
            _views.Add(bulletEntity, bulletView);
            _bulletEntities.Add(bulletEntity.Entity, bulletEntity);
        }

        private void OnEndBulletSimulation(BulletEntity bulletEntity)
        {
            if (_views.ContainsKey(bulletEntity))
            {
                BulletView bulletView = _views[bulletEntity];
                _bulletsViewFactory.Reset(bulletEntity, bulletView);
                _views.Remove(bulletEntity);
                _bulletEntities.Remove(bulletEntity.Entity);
            }
        }

        private void CreateBullet(Bullet bullet)
        {
            _bulletsSimulation.Simulate(bullet, _shipMovement.Position, _shipMovement.Forward);
        }
        
        private void OnDefaultBulletDied(Bullet bullet)
        {
            if (_bulletEntities.ContainsKey(bullet))
            {
                BulletEntity bulletEntity = _bulletEntities[bullet];
                BulletView bulletView = _views[bulletEntity];
                _bulletsViewFactory.Reset(bulletEntity, bulletView);
                _views.Remove(bulletEntity);
                _bulletEntities.Remove(bullet);
            }
        }
    }
}