using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Norne.Implementations
{
    internal class DefaultWatchdog : IWatchdog
    {
        public float Timeout { get; set; }

        public void StartWatchdog(float timeout)
        {
            Timeout = timeout;
            Debug.Log("[Watchdog] ANR Watchdog started.");
        }

        public void RestartApp()
        {
            Process.GetCurrentProcess().Kill();
        }

        public string GetStacktrace()
        {
            return "";
        }
    }
}