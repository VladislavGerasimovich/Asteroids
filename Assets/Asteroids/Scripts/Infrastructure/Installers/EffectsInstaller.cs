using Asteroids.Scripts.Spawners;
using Asteroids.Scripts.ViewFactories.Effects;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class EffectsInstaller : MonoInstaller
    {
        [SerializeField] private EffectsViewFactory effectsViewFactory;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EffectsSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectsViewFactory>().FromInstance(effectsViewFactory).AsSingle();
        }
    }
}