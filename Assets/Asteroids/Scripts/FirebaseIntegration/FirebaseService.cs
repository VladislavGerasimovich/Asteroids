using System;
using Asteroids.Scripts.RewardSystem;
using Asteroids.Scripts.Signals;
using Zenject;

namespace Asteroids.Scripts.FirebaseIntegration
{
    public class FirebaseService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private FirebaseAnalyticsSetter _firebaseAnalyticsSetter;
        private RewardHandler _rewardHandler;

        public FirebaseService(
            SignalBus signalBus,
            FirebaseAnalyticsSetter firebaseAnalyticsSetter,
            RewardHandler rewardHandler)
        {
            _rewardHandler = rewardHandler;
            _firebaseAnalyticsSetter = firebaseAnalyticsSetter;
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
            _firebaseAnalyticsSetter.Set(_rewardHandler.RewardCount);
        }
    }
}