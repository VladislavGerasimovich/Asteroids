using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class Transformable
    {
        public Vector2 Position { get; protected set; }
        public float Rotation { get; protected set; }
    }
}