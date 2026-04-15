using Asteroids.Scripts.SaveSystem;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DataManager>().AsSingle();
        }
    }
}