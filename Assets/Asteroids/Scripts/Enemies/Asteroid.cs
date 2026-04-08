using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public class Asteroid : Enemy
    {
        private Vector2 _direction;
        private bool _inScreen;

        public Asteroid(Vector2 position, float rotation, Vector2 direction, float speed)
        {
            Position = position;
            Rotation = rotation;
            Speed = speed;
            _direction = direction;
        }
        
        public override void Update(float deltaTime)
        {
            Position = Position + _direction * Speed * deltaTime;

            if (Position.x >= 0 && Position.x <= 1 && Position.y >= 0 && Position.y <= 1 && _inScreen == false)
            {
                _inScreen = true;
            }

            if (_inScreen == true)
            {
                if (Position.x < 0 || Position.x > 1 || Position.y < 0 || Position.y > 1)
                {
                    OnEnded?.Invoke(this);
                    _inScreen = false;
                }
            }
        }

        public override void ChangeMovement(Vector2 direction, float time)
        {
            _direction = direction;
        }
    }
    
    public class PartOfAsteroid : Enemy
    {
        private Vector2 _direction;
        private bool _inScreen;

        public PartOfAsteroid(Vector2 position, float rotation, Vector2 direction, float speed)
        {
            Position = position;
            Rotation = rotation;
            Speed = speed;
            _direction = direction;
        }
        
        public override void Update(float deltaTime)
        {
            Position = Position + _direction * Speed * deltaTime;

            if (Position.x >= 0 && Position.x <= 1 && Position.y >= 0 && Position.y <= 1 && _inScreen == false)
            {
                _inScreen = true;
            }

            if (_inScreen == true)
            {
                if (Position.x < 0 || Position.x > 1 || Position.y < 0 || Position.y > 1)
                {
                    OnEnded?.Invoke(this);
                    _inScreen = false;
                }
            }
        }
        
        public override void ChangeMovement(Vector2 direction, float time)
        {
            _direction = direction;
        }
    }
}