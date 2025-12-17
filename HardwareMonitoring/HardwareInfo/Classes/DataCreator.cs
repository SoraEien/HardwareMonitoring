using HardwareMonitoring.Models;
using LibreHardwareMonitor.Hardware;

namespace HardwareMonitoring.HardwareInfo.Classes
{
    public class DataCreator : HardwareManager, IDataCreator
    {
        public IEnumerable<SystemModel> CreateEnumerable()
        {
            var res = new List<SystemModel>();
            foreach (IHardware hardware in _computer.Hardware)
            {
                var sys = new SystemModel();
                sys.Name = hardware.Name;
                sys.Time = DateTime.Now;
                if (hardware.HardwareType == HardwareType.Motherboard)
                    sys.Sensors = new List<SensorModel>(AddMotherboardInfo(hardware));
                else
                    sys.Sensors = new List<SensorModel>(AddSensorInfo(hardware));
                res.Add(sys);
            }

            return res;
        }

        private IEnumerable<SensorModel> AddSensorInfo(IHardware hardware)
        {
            var res = new List<SensorModel>();
            var sensorTypeName = hardware.HardwareType.ToString();

            foreach (ISensor sensor in hardware.Sensors)
                if (sensor.SensorType == SensorType.Load && sensor.Name.Contains(sensorTypeName, StringComparison.OrdinalIgnoreCase) || sensor.SensorType == SensorType.Temperature && CheckSensorValue(sensor))
                    res.Add(CreateSystemInfoModel(sensor));

            return res;
        }

        private IEnumerable<SensorModel> AddMotherboardInfo(IHardware hardware)
        {
            var res = new List<SensorModel>();

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

        private SensorModel CreateSystemInfoModel(ISensor sensor) => new SensorModel
        {
            Name = sensor.Name,
            Value = sensor.Value,
            SensorTypeName = sensor.SensorType
        };
    }
}
