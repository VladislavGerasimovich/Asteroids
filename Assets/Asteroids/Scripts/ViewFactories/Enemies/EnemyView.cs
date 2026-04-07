using Asteroids.Scripts.Enemies;
using UnityEngine;

namespace Asteroids.Scripts.ViewFactories.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _tempViewportPosition;
        
        public Enemy Enemy { get; private set; }
        
        public void Init(Camera camera, Enemy enemy)
        {
            _camera = camera;
            Enemy = enemy;
        }

        public void Update()
        {
            _tempViewportPosition = new Vector3(Enemy.Position.x, Enemy.Position.y, 1);
            transform.position = _camera.ViewportToWorldPoint(_tempViewportPosition);
            transform.rotation = Quaternion.Euler(0, 0, Enemy.Rotation);
        }
    }
}