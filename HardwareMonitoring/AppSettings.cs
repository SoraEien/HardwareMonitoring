namespace HardwareMonitoring
{
    public class AppSettings(long interval, long sendTnterval, bool sendToServer, string serverUrl, string pcName, bool usePcName)
    {
        public long Interval = interval;
        public long SendTnterval = sendTnterval;
        public bool SendToServer = sendToServer;
        public string ServerUrl = serverUrl;
        public string PcName = pcName;
        public bool UsePcName = usePcName;
    }
}
