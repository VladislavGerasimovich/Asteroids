using System;
using Asteroids.Scripts.Signals;
using Zenject;

namespace Asteroids.Scripts.SDK
{
    public class SDKService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private YandexSDK _yandexSDK;

        public SDKService(
            SignalBus signalBus,
            YandexSDK yandexSDK)
        {
            _yandexSDK = yandexSDK;
            _signalBus = signalBus;
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
            _yandexSDK.ShowInterstitialAd();
        }
    }
}