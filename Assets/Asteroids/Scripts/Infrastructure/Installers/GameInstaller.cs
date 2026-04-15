using Asteroids.Scripts.ResolutionHandler;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScreenResolutionHandler>().AsSingle();
        }
    }
}