using System;
using Asteroids.Scripts.Signals;
using MVVM;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace Asteroids.Scripts.GameLoop
{
    public class GameLoopHandler : IInitializable, IDisposable
    {
        [Data("IsPlayerDied")]
        public ReactiveProperty<bool> IsPlayerDied;
        
        private SignalBus _signalBus;

        public GameLoopHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
            IsPlayerDied = new ReactiveProperty<bool>();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        private void OnPlayerDied()
        {
            IsPlayerDied.Value = true;
        }

        [Method("RestartButton")]
        public void OnRestartButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _signalBus.Fire<OnRestartButtonClickedSignal>();
        }
    }
}