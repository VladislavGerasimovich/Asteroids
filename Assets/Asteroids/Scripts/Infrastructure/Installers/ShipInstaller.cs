using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.PlayerHealthSystem;
using Asteroids.Scripts.PlayerShip;
using Asteroids.Scripts.PlayerShipEffects;
using Asteroids.Scripts.ViewFactories.Bullets;
using Asteroids.Scripts.ViewFactories.PlayerHealth;
using Asteroids.Scripts.ViewModels;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private MobileInputView mobileInputView;
        [SerializeField] private BulletsViewFactory bulletsViewFactory;
        [SerializeField] private PlayerHealthView playerHealthView;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.Bind<ShipMovement>().AsSingle();
            Container.Bind<InertMovement>().AsSingle();
            Container.Bind<InputFactory>().AsSingle();
            Container.Bind<MobileInputView>().FromInstance(mobileInputView).AsSingle();
            Container.BindInterfacesTo<PlayerShipEffectsHandler>().AsSingle();
            Container.BindInterfacesTo<ShipInputRouter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunRollbackViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<InertMovementViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealthViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PostCollisionMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipWeaponsHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputBlocker>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSpawner>().FromInstance(playerSpawner).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletsViewFactory>().FromInstance(bulletsViewFactory).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealth>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealthsSpawner>().AsSingle();
            Container.Bind<PlayerHealthView>().FromInstance(playerHealthView).AsSingle();
        }
    }
}