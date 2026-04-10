using UnityEngine;

namespace Asteroids.Scripts.ViewFactories.Player
{
    public class PlayerShipView : MonoBehaviour
    {
        private Camera _mainCamera;
        private Vector3 _tempViewportPosition;
        
        public BoxCollider2D Collider { get; private set; }

        private void Awake()
        {
            _mainCamera = Camera.main;
            Collider = GetComponent<BoxCollider2D>();
        }

        public void Move(Vector2 position, float rotation)
        {
            _tempViewportPosition = new Vector3(position.x, position.y, 1);
            transform.position = _mainCamera.ViewportToWorldPoint(_tempViewportPosition);
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}