using System;
using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public abstract class Enemy
    {
        public Vector2 Position { get; protected set; }
        public float Rotation { get; protected set; }
        public float Speed { get; protected set; }
        
        public Action<Enemy> OnEnded;

        public abstract void Update(float deltaTime);
        public abstract void ChangeMovement(Vector2 direction, float time);
    }
}