using HardwareTemperature.HardwareInfo;
using LibreHardwareMonitor.Hardware;

namespace HardwareMonitoringClient.HardwareInfo
{
    public class HardwareManager
    {
        private Computer _computer;

        public HardwareManager()
        {
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMotherboardEnabled = true,
            };
            _computer.Open();
        }

        public void Close() => _computer.Close();

        public async Task Monitor()
        {
            _computer.Accept(new UpdateVisitor());

            foreach (IHardware hardware in _computer.Hardware)
            {
                Console.WriteLine("{0}", hardware.Name);

                if (hardware.HardwareType == HardwareType.Motherboard)
                {
                    foreach (IHardware subhardware in hardware.SubHardware)
                    {
                        foreach (ISensor sensor in subhardware.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature)
                                Console.WriteLine("\t{0}, temperature: {1} C", sensor.Name, sensor.Value);
                        }
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                    if (sensor.Name.Contains("GPU") || sensor.Name.Contains("CPU") || sensor.SensorType == SensorType.Temperature)
                        Console.WriteLine("\t{0}, {2}: {1}", sensor.Name, sensor.Value, sensor.SensorType);
            }
        }
    }
}
