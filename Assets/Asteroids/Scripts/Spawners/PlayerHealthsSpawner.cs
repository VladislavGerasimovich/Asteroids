using Asteroids.Scripts.PlayerHealthSystem;
using Asteroids.Scripts.ViewFactories.PlayerHealth;
using MVVM;
using Zenject;

namespace Asteroids.Scripts.Enemies
{
    public class PlayerHealthsSpawner : IInitializable
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
        }
        
        [Method("OnHealthReduced")]
        public void ReduceHeart(int count)
        {
            _playerHealthView.HideHeart();
        }
    }
}