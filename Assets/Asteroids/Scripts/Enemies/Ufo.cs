using Asteroids.Scripts.PlayerShip;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public class Ufo : Enemy
    {
        private TransformData _player;
        private Vector2 _direction;
        private bool _hasBounced;
        private float _speedMultiplier;
        private float _bounceMultiplier;
        private float _normalMultiplier;
        
        public Ufo(Vector2 position, float rotation, float speed, TransformData player)
        {
            Position = position;
            Rotation = rotation;
            _player = player;
            Speed = speed;
            _normalMultiplier = 1f;
            _speedMultiplier = _normalMultiplier;
            _bounceMultiplier = 2f;
            Type = EnemyType.Ufo;
        }

        public override void Update(float deltaTime)
        {
            if (!_hasBounced)
            {
                _direction = (_player.Position - Position).normalized;
            }
            
            Position += _direction * Time.deltaTime * Speed * _speedMultiplier;
        }
        
        public override void ChangeMovement(Vector2 direction, float time)
        {
            _direction = direction;
            ChangeSpeed(time);
        }
        
        private void LookAt(Vector2 point)
        {
            Rotate(Vector2.SignedAngle(Quaternion.Euler(0, 0, Rotation) * Vector3.up, (point - Position)));
        }

        private void Rotate(float delta)
        {
            Rotation = Mathf.Repeat(Rotation + delta, 360);
        }

        private async void ChangeSpeed(float time)
        {
            _hasBounced = true;
            _speedMultiplier = _bounceMultiplier;
            
            while (time > 0)
            {
                time -= Time.deltaTime;
                _speedMultiplier -= Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            _speedMultiplier = _normalMultiplier;
            _hasBounced = false;
        }
    }
}