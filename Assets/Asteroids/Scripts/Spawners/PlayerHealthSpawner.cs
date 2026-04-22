using Asteroids.Scripts.PlayerHealthSystem;
using Asteroids.Scripts.ViewFactories.PlayerHealth;
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
    }
}