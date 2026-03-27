using UnityEngine;

namespace Asteroids.Scripts.PlayerShip
{
    public class PlayerShipView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        
        private Vector3 _tempViewportPosition;
        
        public void Move(Vector2 position, float rotation)
        {
            _tempViewportPosition = new Vector3(position.x, position.y, 1);
            transform.position = mainCamera.ViewportToWorldPoint(_tempViewportPosition);
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}