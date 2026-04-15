using System;
using Asteroids.Scripts.SaveSystem;
using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class ShipMovement : TransformData
    {
        private readonly float _degreesPerSecond = 180;
        
        public Vector2 Forward => Quaternion.Euler(0, 0, Rotation) * Vector3.up;
        
        public ShipMovement(DataManager dataManager)
        {
            dataManager.LoadProgressOrInitNew();
            Position = dataManager.PlayerPosition;
            Rotation = dataManager.PlayerRotation;
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