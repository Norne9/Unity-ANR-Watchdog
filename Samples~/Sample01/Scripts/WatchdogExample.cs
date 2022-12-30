using UnityEngine;
using UnityEngine.UI;

namespace Norne
{
    public class WatchdogExample : MonoBehaviour
    {
        [SerializeField]
        private TextSlider anrTimeout;

        [SerializeField]
        private TextSlider anrEmulation;

        [SerializeField]
        private Button anrButton;

        [SerializeField]
        private Button copyAnrButton;

        private void Start()
        {
            anrTimeout.Value = Watchdog.Timeout;
            anrTimeout.OnValueChanged += timeout => Watchdog.Timeout = timeout;
            anrEmulation.Value = Watchdog.Timeout + 1f;

            anrButton.onClick.AddListener(() =>
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                using var pluginClass = new AndroidJavaClass("com.norne.anrwatchdog.ANREmulator");
                pluginClass.CallStatic("sleep", (long) (anrEmulation.Value * 1000));
#endif
            });

            copyAnrButton.onClick.AddListener(() =>
            {
                GUIUtility.systemCopyBuffer = Watchdog.TryGetStacktrace(out var stacktrace)
                    ? stacktrace
                    : "No stacktrace found";
            });
        }
    }
}