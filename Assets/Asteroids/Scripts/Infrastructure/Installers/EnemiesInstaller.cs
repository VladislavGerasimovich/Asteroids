using Asteroids.Scripts.Enemies;
using Asteroids.Scripts.ViewFactories.Enemies;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private EnemiesViewFactory enemiesViewFactory;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EnemiesSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesViewFactory>().FromInstance(enemiesViewFactory).AsSingle();
        }
    }
}