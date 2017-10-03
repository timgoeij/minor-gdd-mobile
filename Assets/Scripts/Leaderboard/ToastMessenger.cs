using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColourRun.Leaderboard
{
    public class ToastMessenger : MonoBehaviour
    {
        private string toastString = "";
        private AndroidJavaObject currentActivity;

        public void ShowToast(string message)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                ShowToastOnUIThread(message);
            }
        }

        private void ShowToastOnUIThread(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            toastString = message;

            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(ShowToast));
        }

        private void ShowToast()
        {
            AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");

            AndroidJavaObject toastMessage = new AndroidJavaObject("java.lang.String", toastString);

            AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, toastMessage, Toast.GetStatic<int>("LENGTH_SHORT"));
            toast.Call("show");
        }
    }
}
