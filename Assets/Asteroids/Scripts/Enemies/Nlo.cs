using Asteroids.Scripts.PlayerShip;
using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public class Nlo : Enemy
    {
        private TransformData _player;
        
        public Nlo(Vector2 position, float rotation, float speed, TransformData player)
        {
            Position = position;
            Rotation = rotation;
            _player = player;
            Speed = speed;
        }

        public override void Update(float deltaTime)
        {
            Position = Vector3.MoveTowards(Position, _player.Position, Speed * deltaTime);
            LookAt(_player.Position);
        }
        
        private void LookAt(Vector2 point)
        {
            Rotate(Vector2.SignedAngle(Quaternion.Euler(0, 0, Rotation) * Vector3.up, (point - Position)));
        }

        private void Rotate(float delta)
        {
            Rotation = Mathf.Repeat(Rotation + delta, 360);
        }
    }
}