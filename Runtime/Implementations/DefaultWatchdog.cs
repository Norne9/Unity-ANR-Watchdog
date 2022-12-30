using UnityEngine;

namespace Norne.Implementations
{
    internal class DefaultWatchdog : IWatchdog
    {
        private float _timeout;

        public float Timeout
        {
            get => _timeout;
            set
            {
                Debug.Log($"[Watchdog] Timeout set to {_timeout:F2}s");
                _timeout = value;
            }
        }

        public void StartWatchdog(float timeout)
        {
            Timeout = timeout;

            Debug.Log("[Watchdog] ANR Watchdog started.");
        }

        public string GetStacktrace()
        {
            return string.Empty;
        }
    }
}