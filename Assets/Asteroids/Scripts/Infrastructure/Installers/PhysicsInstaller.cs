using Asteroids.Scripts.OwnPhysics;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class PhysicsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PhysicsRouter>().AsSingle();
            Container.Bind<CollisionsRecords>().AsSingle();
        }
    }
}