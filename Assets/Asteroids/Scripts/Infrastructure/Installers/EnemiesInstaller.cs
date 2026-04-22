using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.Spawners;
using Asteroids.Scripts.SpawnTimers;
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
            Container.BindInterfacesTo<EnemyCollisionWithPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnTimer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyViewCreator>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemiesViewFactory>().FromInstance(enemiesViewFactory).AsSingle();
        }
    }
}