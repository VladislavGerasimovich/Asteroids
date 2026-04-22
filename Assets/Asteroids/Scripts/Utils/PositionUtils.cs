using UnityEngine;

namespace Asteroids.Scripts.Utils
{
    public class PositionUtils
    {
        private Vector2 _circlePositionOffset;
        private float _positionMinOffset;
        private float _positionMaxOffset;
        
        public PositionUtils()
        {
            _circlePositionOffset = new Vector2(0.5f, 0.5f);
            _positionMinOffset = 0.1f;
            _positionMaxOffset = 0.9f;
        }
        
        public Vector2 GetRandomPositionInsideUnitCircle()
        {
            return Random.insideUnitCircle.normalized + _circlePositionOffset;
        }
        
        public Vector2 GetDirectionThroughScreen(Vector2 position)
        {
            return (new Vector2(Random.Range(_positionMinOffset, _positionMaxOffset), Random.Range(_positionMinOffset, _positionMaxOffset)) - position).normalized;
        }
    }
}