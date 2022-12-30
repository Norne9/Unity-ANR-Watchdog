namespace Norne
{
    internal interface IWatchdog
    {
        public float Timeout { get; set; }
        public void StartWatchdog(float timeout);
        public string GetStacktrace();
    }
}