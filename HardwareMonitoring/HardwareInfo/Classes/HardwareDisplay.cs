namespace HardwareMonitoring.HardwareInfo.Classes
{
    public class HardwareDisplay : IHardwareDisplay
    {
        private readonly IHardwareManager _hardwareManager;

        public HardwareDisplay(IHardwareManager manager)
        {
            _hardwareManager = manager;
        }

        public async Task DisplaySensorsValues()
        {
            Console.Clear();
            Console.WriteLine("{0}\n", DateTime.Now);

            _hardwareManager.Update();
            await _hardwareManager.Monitor();

            Console.WriteLine("Press Enter to exit");
        }

        public async Task SentToServer()
        {
            Console.WriteLine("Sent to server");
        }
    }
}
