using System.Runtime.InteropServices;

public class YandexSDK
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();

    public void ShowInterstitalAd()
    {
#if !UNITY_EDITOR
        ShowAdv();
#endif
    }
}