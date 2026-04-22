using Asteroids.Scripts.SaveSystem;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameBalanceConfig>().AsSingle();
            Container.Bind<SaveDataRepository>().AsSingle();
            Container.Bind<SaveBootstrapper>().AsSingle();
            Container.BindInterfacesTo<DebugSaveResetService>().AsSingle();
        }
    }
}