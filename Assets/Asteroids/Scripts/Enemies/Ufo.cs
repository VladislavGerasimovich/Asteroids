using Asteroids.Scripts.PlayerShip;
using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public class Ufo : Enemy
    {
        private UfoBounceHandler _ufoBounceHandler;
        private TransformData _player;
        private Vector2 _direction;
        
        public Ufo(Vector2 position, float rotation, float speed, TransformData player)
        {
            Position = position;
            Rotation = rotation;
            _player = player;
            Speed = speed;
            
            Type = EnemyType.Ufo;
            _ufoBounceHandler = new UfoBounceHandler();
        }

        public override void Update(float deltaTime)
        {
            if (!_ufoBounceHandler.HasBounced)
            {
                _direction = (_player.Position - Position).normalized;
            }
            
            Position += _direction * deltaTime * Speed * _ufoBounceHandler.SpeedMultiplier;
        }

        public override void Dispose()
        {
            _ufoBounceHandler.Dispose();
        }
        
        public override void ChangeMovement(Vector2 direction, float time)
        {
            _direction = direction;
            _ufoBounceHandler.Perform(time);
        }
    }
}