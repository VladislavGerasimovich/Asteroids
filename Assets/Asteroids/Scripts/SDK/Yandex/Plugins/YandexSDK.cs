using System.Runtime.InteropServices;

namespace Asteroids.Scripts.SDK
{
    public class YandexSDK
    {
        [DllImport("__Internal")]
        private static extern void ShowAdv();

        public void ShowInterstitialAd()
        {
#if !UNITY_EDITOR
        ShowAdv();
#endif
        }
    }
}