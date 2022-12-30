using Norne.Implementations;
using UnityEngine;

namespace Norne
{
    public static class Watchdog
    {
        private static IWatchdog _watchdog;

        public static float Timeout
        {
            get
            {
                if (_watchdog != null) return _watchdog.Timeout;
                Debug.LogError("[Watchdog] Not started.");
                return 0f;
            }
            set
            {
                if (_watchdog == null)
                {
                    Debug.LogError("[Watchdog] Not started.");
                    return;
                }

                if (Mathf.Abs(value - _watchdog.Timeout) > 0.01f) _watchdog.Timeout = value;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Start()
        {
            if (_watchdog != null) return;

#if UNITY_ANDROID && !UNITY_EDITOR
            _watchdog = new AndroidWatchdog();
#else
            _watchdog = new DefaultWatchdog();
#endif
            _watchdog.StartWatchdog(9f);
        }

        public static bool TryGetStacktrace(out string stacktrace)
        {
            stacktrace = _watchdog.GetStacktrace();
            return !string.IsNullOrEmpty(stacktrace);
        }
    }
}