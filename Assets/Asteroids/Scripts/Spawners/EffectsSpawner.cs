using Asteroids.Scripts.PlayerShip;
using Asteroids.Scripts.ViewFactories.Effects;
using UnityEngine;
using Asteroids.Scripts.Timers;
using Zenject;

namespace Asteroids.Scripts.Enemies
{
    public class EffectsSpawner : ITickable
    {
        private readonly Timers<EffectView> _timers = new Timers<EffectView>();
        private EffectsViewFactory _effectsViewFactory;
        private Camera _camera;

        public EffectsSpawner(Camera camera, EffectsViewFactory effectsViewFactory)
        {
            _camera = camera;
            _effectsViewFactory = effectsViewFactory;
        }
        
        public void Tick()
        {
            _timers.Tick(Time.deltaTime);
        }

        public void CreateView(Effects effect, TransformData transformData)
        {
            EffectView effectView = _effectsViewFactory.GetTemplate(effect);
            effectView.Init(_camera, transformData, effect);
            _timers.Start(effectView, 3, OnEffectEnded);
            effectView.gameObject.SetActive(true);
        }

        private void OnEffectEnded(EffectView effectView)
        {
            _effectsViewFactory.Reset(effectView);
        }
    }
}