using Norne.Implementations;
using UnityEngine;

namespace Norne
{
    /// <summary>
    ///     This package will allow you to reduce the number of ANRs of your application.
    ///     This is achieved by using a watchdog timer which will restart the application in case something goes wrong.
    /// </summary>
    public static class Watchdog
    {
        private static IWatchdog _watchdog;

        /// <summary>
        ///     The time after which the application will be restarted. Specified in seconds.
        ///     The default value is 9 seconds.
        /// </summary>
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

        /// <summary>
        ///     Allows you to get the stack trace of an application if it has terminated on a watchdog timer.
        /// </summary>
        /// <param name="stacktrace">Stack trace of the main java thread of an application. In JSON format.</param>
        /// <returns>True in case an application has terminated on a watchdog timer. False otherwise.</returns>
        public static bool TryGetStacktrace(out string stacktrace)
        {
            stacktrace = _watchdog.GetStacktrace();
            return !string.IsNullOrEmpty(stacktrace);
        }
    }
}