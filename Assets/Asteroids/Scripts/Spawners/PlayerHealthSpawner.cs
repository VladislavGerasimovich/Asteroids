using Asteroids.Scripts.PlayerHealthSystem;
using Asteroids.Scripts.ViewFactories.PlayerHealth;
using MVVM;
using Zenject;

namespace Asteroids.Scripts.Spawners
{
    public class PlayerHealthSpawner : IInitializable
    {
        private PlayerHealthView _playerHealthView;
        private PlayerHealth _playerHealth;

        public PlayerHealthSpawner(PlayerHealthView playerHealthView, PlayerHealth playerHealth)
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
            if (count >= 0)
            {
                _playerHealthView.HideHeart();
            }
        }
    }
}