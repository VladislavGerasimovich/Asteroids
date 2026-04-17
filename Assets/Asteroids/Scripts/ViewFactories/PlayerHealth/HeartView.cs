using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Scripts.ViewFactories.PlayerHealth
{
    public class HeartView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private float fillTime;

        private float _accumulatedTime;

        public bool IsActive { get; private set; }

        public void Init()
        {
            icon.fillAmount = 0;
        }

        public void Show()
        {
            Fill();
        }

        public void Hide()
        {
            Empty();
        }

        private async UniTask Fill()
        {
            _accumulatedTime = 0;

            while (_accumulatedTime <= fillTime)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                _accumulatedTime += Time.deltaTime;
                icon.fillAmount = _accumulatedTime / fillTime;
            }

            IsActive = true;
        }

        private async UniTask Empty()
        {
            _accumulatedTime = fillTime;

            while (_accumulatedTime >= 0)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                _accumulatedTime -= Time.deltaTime;

                if (icon != null)
                {
                    icon.fillAmount = _accumulatedTime / fillTime;
                }
            }

            IsActive = false;
        }
    }
}