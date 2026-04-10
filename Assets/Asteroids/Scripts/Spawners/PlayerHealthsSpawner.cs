using System;
using Asteroids.Scripts.PlayerHealthSystem;
using Asteroids.Scripts.ViewFactories.PlayerHealth;
using Zenject;

namespace Asteroids.Scripts.Enemies
{
    public class PlayerHealthsSpawner : IInitializable, IDisposable
    {
        private PlayerHealthView _playerHealthView;
        private PlayerHealth _playerHealth;

        public PlayerHealthsSpawner(PlayerHealthView playerHealthView, PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealthView = playerHealthView;
        }

        public void Initialize()
        {
            _playerHealthView.CreateViews((int)_playerHealth.Health);
            _playerHealth.OnHealthChanged += ReduceHeart;
        }

        public void Dispose()
        {
            _playerHealth.OnHealthChanged -= ReduceHeart;
        }

        private void ReduceHeart()
        {
            _playerHealthView.HideHeart();
        }
    }
}