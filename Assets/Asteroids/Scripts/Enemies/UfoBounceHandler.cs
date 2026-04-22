using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Asteroids.Scripts.Enemies
{
    public class UfoBounceHandler
    {
        private float _bounceMultiplier;
        private float _normalMultiplier;
        
        public bool HasBounced { get; private set; }
        public float SpeedMultiplier { get; private set; }

        public UfoBounceHandler()
        {
            _normalMultiplier = 1f;
            SpeedMultiplier = _normalMultiplier;
            _bounceMultiplier = 2f;
        }

        public void Perform(float time)
        {
            ChangeSpeed(time);
        }
        
        private async UniTask ChangeSpeed(float time)
        {
            HasBounced = true;
            SpeedMultiplier = _bounceMultiplier;
            
            while (time > 0)
            {
                time -= Time.deltaTime;
                SpeedMultiplier -= Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            SpeedMultiplier = _normalMultiplier;
            HasBounced = false;
        }
    }
}