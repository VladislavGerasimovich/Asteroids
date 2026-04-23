using System;
using Asteroids.Scripts.PlayerShip;
using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public abstract class Enemy : TransformData
    {
        public Action<Enemy> OnEnded;

        public float Speed { get; protected set; }
        public EnemyType Type { get; protected set; }

        public abstract void Update(float deltaTime);
        public abstract void Dispose();
        public abstract void ChangeMovement(Vector2 direction, float time);
    }

    public enum EnemyType
    {
        Ufo,
        Asteroid,
        PartOfAsteroid
    }
}