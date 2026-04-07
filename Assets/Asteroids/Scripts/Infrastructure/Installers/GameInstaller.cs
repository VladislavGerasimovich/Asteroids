using Asteroids.Scripts.OwnPhysics;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PhysicsRouter>().AsSingle();
        }
    }
}