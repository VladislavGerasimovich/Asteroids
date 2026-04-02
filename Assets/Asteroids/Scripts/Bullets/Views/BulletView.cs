using UnityEngine;

namespace Asteroids.Scripts.Bullets.Views
{
    public class BulletView : MonoBehaviour
    {
        private Camera _mainCamera;
        private Vector3 _tempViewportPosition;
        private Trajectory _trajectory;
        private float _angle;

        public void OnEnable()
        {
            _mainCamera = Camera.main;
        }

        public void Init(Trajectory trajectory)
        {
            _trajectory = trajectory;
        }

        public void Update()
        {
            _tempViewportPosition = new Vector3(_trajectory.Position.x, _trajectory.Position.y, 1);
            transform.position = _mainCamera.ViewportToWorldPoint(_tempViewportPosition);
            _angle = Vector2.SignedAngle(Vector2.up, _trajectory.Direction);
            transform.rotation = Quaternion.Euler(0, 0, _angle);
        }
    }
}