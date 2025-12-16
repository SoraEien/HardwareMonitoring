using HardwareMonitoring.Models;
using LibreHardwareMonitor.Hardware;

namespace HardwareMonitoring.HardwareInfo.Classes
{
    public class DataCreator : HardwareManager, IDataCreator
    {
        public IEnumerable<SystemInfoModel> CreateEnumerable()
        {
            var res = new List<SystemInfoModel>();
            foreach (IHardware hardware in _computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Motherboard)
                    res.AddRange(AddMotherboardInfo(hardware));
                else
                    res.AddRange(AddSensorInfo(hardware));
            }
            return res;
        }

        private IEnumerable<SystemInfoModel> AddSensorInfo(IHardware hardware)
        {
            var res = new List<SystemInfoModel>();
            var sensorTypeName = hardware.HardwareType.ToString();

            foreach (ISensor sensor in hardware.Sensors)
                if (sensor.SensorType == SensorType.Load && sensor.Name.Contains(sensorTypeName, StringComparison.OrdinalIgnoreCase) || sensor.SensorType == SensorType.Temperature && CheckSensorValue(sensor))
                    res.Add(CreateSystemInfoModel(sensor));

            return res;
        }

        private IEnumerable<SystemInfoModel> AddMotherboardInfo(IHardware hardware)
        {
            var res = new List<SystemInfoModel>();

            foreach (ISensor sensor in hardware.Sensors)
                if ((sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Temperature) && CheckSensorValue(sensor))
                    res.Add(CreateSystemInfoModel(sensor));

            foreach (IHardware subhardware in hardware.SubHardware)
                foreach (ISensor sensor in subhardware.Sensors)
                    if ((sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Temperature) && CheckSensorValue(sensor))
                        res.Add(CreateSystemInfoModel(sensor));

            return res;
        }

        private bool CheckSensorValue(ISensor sensor) => sensor.Value.HasValue && sensor.Value.Value > 0;

        private SystemInfoModel CreateSystemInfoModel(ISensor sensor) => new SystemInfoModel
        {
            Name = sensor.Name,
            Value = sensor.Value,
            SensorTypeName = sensor.SensorType
        };
    }
}
