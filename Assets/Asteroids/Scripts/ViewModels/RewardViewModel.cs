using System;
using Asteroids.Scripts.RewardSystem;
using MVVM;
using UniRx;
using Zenject;

namespace Asteroids.Scripts.ViewModels
{
    public class RewardViewModel : IInitializable, IDisposable
    {
        [Data("RewardsCount")]
        public ReactiveProperty<string> RewardsCount;
        
        private RewardHandler _rewardHandler;

        public RewardViewModel(RewardHandler rewardHandler)
        {
            _rewardHandler = rewardHandler;
            RewardsCount = new ReactiveProperty<string>();
        }

        public void Initialize()
        {
            _rewardHandler.OnCountChanged += OnRewardChanged;
        }

        public void Dispose()
        {
            _rewardHandler.OnCountChanged -= OnRewardChanged;
        }

        private void OnRewardChanged()
        {
            RewardsCount.Value = _rewardHandler.RewardCount.ToString();
        }
    }
}