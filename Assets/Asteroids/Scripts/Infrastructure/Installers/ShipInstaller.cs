using Asteroids.Scripts.PlayerShip;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class ShipInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerShipView playerShipView;
        [SerializeField] private MobileJoystick mobileJoystick;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ShipInputRouter>().AsSingle();
            Container.Bind<ShipMovement>().AsSingle();
            Container.Bind<PlayerShipView>().FromInstance(playerShipView).AsSingle();
            Container.Bind<MobileJoystick>().FromInstance(mobileJoystick).AsSingle();
        }
    }
}