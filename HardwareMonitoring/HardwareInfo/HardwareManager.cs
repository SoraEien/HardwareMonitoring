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
                    await WriteMotherboardInfo(hardware);
                else
                    await WriteSensorInfo(hardware);
            }
        }

        private async Task WriteSensorInfo(IHardware hardware)
        {
            var sensorTypeName = hardware.HardwareType.ToString();

            foreach (ISensor sensor in hardware.Sensors)
                if ((sensor.SensorType == SensorType.Load && sensor.Name.Contains(sensorTypeName, StringComparison.OrdinalIgnoreCase)) || (sensor.SensorType == SensorType.Temperature && CheckSensorValue(sensor)))
                    Console.WriteLine("\t{0}, {2}: {1}", sensor.Name, sensor.Value, sensor.SensorType);
        }

        private async Task WriteMotherboardInfo(IHardware hardware)
        {
            foreach (ISensor sensor in hardware.Sensors)
                if ((sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Temperature) && CheckSensorValue(sensor))
                    Console.WriteLine("\t{0}, {2}: {1}", sensor.Name, sensor.Value, sensor.SensorType);

            foreach (IHardware subhardware in hardware.SubHardware)
                foreach (ISensor sensor in subhardware.Sensors)
                    if ((sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Temperature) && CheckSensorValue(sensor))
                        Console.WriteLine("\t{0}, {2}: {1}", sensor.Name, sensor.Value, sensor.SensorType);
        }

        private bool CheckSensorValue(ISensor sensor) => sensor.Value.HasValue && sensor.Value.Value > 0;
    }
}
