using System;
using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class ShipMovement : Transformable
    {
        private readonly float _degreesPerSecond = 180;
        
        public Vector2 Forward => Quaternion.Euler(0, 0, Rotation) * Vector3.up;
        
        public ShipMovement()
        {
            Position = new Vector2(0.5f, 0.5f);
        }
        
        public void MoveLooped(Vector2 delta)
        {
            var nextPosition = Position + delta;

            nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
            nextPosition.y = Mathf.Repeat(nextPosition.y, 1);

            Position = nextPosition;
        }
        
        public void Rotate(float direction) 
        {
            if (direction == 0)
                throw new InvalidOperationException(nameof(direction));

            direction = direction > 0 ? -1 : 1;

            Rotation = Mathf.Repeat(Rotation + (direction * Time.deltaTime * _degreesPerSecond), 360);
        }
    }
}