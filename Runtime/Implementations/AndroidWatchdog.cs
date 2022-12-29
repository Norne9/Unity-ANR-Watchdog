#if UNITY_ANDROID
using UnityEngine;

namespace Norne.Implementations
{
    internal class AndroidWatchdog : IWatchdog
    {
        private float _timeout;
        private AndroidJavaObject _watchdog;

        public float Timeout
        {
            get => _timeout;
            set
            {
                if (_watchdog == null) return;

                _watchdog.Call("setTimeout", (long) (value * 1000f));
                _timeout = value;
            }
        }

        public void StartWatchdog(float timeout)
        {
            _timeout = timeout;

            using var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            using var activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");

            _watchdog = new AndroidJavaObject("com.norne.anrwatchdog.Watchdog");
            _watchdog.Call("setContext", activityContext);
            _watchdog.Call("startWatchdog", (long) (timeout * 1000f));
        }

        public void RestartApp()
        {
            _watchdog.Call("restartApp");
        }

        public string GetStacktrace()
        {
            return _watchdog.Call<string>("getStacktrace");
        }
    }
}
#endif