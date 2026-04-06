using Asteroids.Scripts.Bullets;
using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.PlayerShip;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerShipView playerShipView;
        [SerializeField] private MobileInputView mobileInputView;
        [SerializeField] private BulletsViewFactory bulletsViewFactory;
        [SerializeField] private EnemiesViewFactory enemiesViewFactory;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.BindInterfacesTo<ShipInputRouter>().AsSingle();
            Container.Bind<ShipMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipWeaponsHandler>().AsSingle();
            Container.Bind<PlayerShipView>().FromInstance(playerShipView).AsSingle();
            Container.Bind<MobileInputView>().FromInstance(mobileInputView).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletsViewFactory>().FromInstance(bulletsViewFactory).AsSingle();
            Container.BindInterfacesTo<EnemiesSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesViewFactory>().FromInstance(enemiesViewFactory).AsSingle();
        }
    }
}