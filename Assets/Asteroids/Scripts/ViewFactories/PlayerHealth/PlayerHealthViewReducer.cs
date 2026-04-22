using MVVM;

namespace Asteroids.Scripts.ViewFactories.PlayerHealth
{
    public class PlayerHealthViewReducer
    {
        private PlayerHealthView _playerHealthView;

        public PlayerHealthViewReducer(PlayerHealthView playerHealthView)
        {
            _playerHealthView = playerHealthView;
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