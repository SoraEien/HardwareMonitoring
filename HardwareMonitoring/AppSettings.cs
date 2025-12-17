namespace HardwareMonitoring
{
    public class AppSettings(long interval, long sendTnterval, bool sendToServer, string serverUrl)
    {
        public long Interval = interval;
        public long SendTnterval = sendTnterval;
        public bool SendToServer = sendToServer;
        public string ServerUrl = serverUrl;
    }
}
