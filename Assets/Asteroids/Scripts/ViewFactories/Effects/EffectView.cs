using Asteroids.Scripts.PlayerShip;
using UnityEngine;

namespace Asteroids.Scripts.ViewFactories.Effects
{
    public class EffectView : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _tempViewportPosition;
        private TransformData _transformData;
        
        public Effects Effect { get; private set; }

        public void Init(Camera camera, TransformData transformData, Effects effect)
        {
            _transformData = transformData;
            _camera = camera;
            Effect = effect;
        }

        public void Update()
        {
            _tempViewportPosition = new Vector3(_transformData.Position.x, _transformData.Position.y, 1);
            transform.position = _camera.ViewportToWorldPoint(_tempViewportPosition);
            transform.rotation = Quaternion.Euler(0, 0, _transformData.Rotation);
        }
    }
}