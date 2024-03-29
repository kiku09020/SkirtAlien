using UnityEngine;

/* ★スマホ振動に関するスクリプトです */
public class Vibration : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    // 端末の振動
    public static void Vibrate(long milliseconds)
    {
		if(isAndroid()) {
            vibrator.Call("vibrate", milliseconds);
		}

        else {
            Handheld.Vibrate();
        }
    }

    // 端末がAndroidかどうか
    static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
}
