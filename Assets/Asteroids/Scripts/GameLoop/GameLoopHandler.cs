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
        private YandexSDK _yandexSDK;

        public GameLoopHandler(SignalBus signalBus, YandexSDK yandexSDK)
        {
            _yandexSDK = yandexSDK;
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
            _yandexSDK.ShowInterstitalAd();
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