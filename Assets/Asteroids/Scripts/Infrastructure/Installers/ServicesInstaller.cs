using Asteroids.Scripts.FirebaseIntegration;
using Asteroids.Scripts.SDK;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirebaseService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SDKService>().AsSingle();
        }
    }
}