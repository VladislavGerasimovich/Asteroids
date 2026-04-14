using System;
using Asteroids.Scripts.PlayerHealthSystem;
using MVVM;
using UniRx;
using Zenject;

namespace Asteroids.Scripts.ViewModels
{
    public class PlayerHealthViewModel : IInitializable, IDisposable
    {
        [Data("OnHealthReduced")]
        public readonly ReactiveProperty<int> Health;
        
        private PlayerHealth _playerHealth;

        public PlayerHealthViewModel(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            Health = new ReactiveProperty<int>(0);
        }
        
        public void Initialize()
        {
            _playerHealth.OnReduced += OnHealthReduced;
        }

        public void Dispose()
        {
            _playerHealth.OnReduced -= OnHealthReduced;
        }

        private void OnHealthReduced()
        {
            Health.Value = (int)_playerHealth.Health;
        }
    }
}