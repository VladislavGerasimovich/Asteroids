using System;
using Asteroids.Scripts.PlayerShip;
using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public abstract class Enemy : TransformData
    {
        public float Speed { get; protected set; }
        
        public Action<Enemy> OnEnded;

        public abstract void Update(float deltaTime);
        public abstract void ChangeMovement(Vector2 direction, float time);
    }
}