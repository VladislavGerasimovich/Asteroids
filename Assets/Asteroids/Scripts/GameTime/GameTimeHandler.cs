using System;
using Asteroids.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.GameTime
{
    public class GameTimeHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;

        public GameTimeHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
            _signalBus.Subscribe<OnRestartButtonClickedSignal>(OnRestartButtonClicked);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
            _signalBus.Unsubscribe<OnRestartButtonClickedSignal>(OnRestartButtonClicked);
        }

        private void OnPlayerDied()
        {
            Time.timeScale = 0;
        }

        private void OnRestartButtonClicked()
        {
            Time.timeScale = 1;
        }
    }
}