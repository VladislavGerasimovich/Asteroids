using Asteroids.Scripts.Bullets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.ViewFactories.Bullets
{
    public class BulletView : MonoBehaviour
    {
        private Camera _mainCamera;
        private Vector3 _tempViewportPosition;
        private Trajectory _trajectory;
        private float _angle;
        private Vector2 _positionOffset;

        public void OnEnable()
        {
            _mainCamera = Camera.main;
        }

        private void Awake()
        {
            _positionOffset = new Vector2(0.5f, 0.5f);
        }

        private void OnDisable()
        {
            ResetPosition();
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

        private void ResetPosition()
        {
            if (_mainCamera != null)
            {
                transform.position = _mainCamera.ViewportToWorldPoint(Random.insideUnitCircle.normalized + _positionOffset);
            }
        }
    }
}