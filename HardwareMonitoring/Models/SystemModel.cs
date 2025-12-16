namespace HardwareMonitoring.Models
{
    public class SystemModel
    {
        private string _name;
        private IEnumerable<SensorModel> _sensors;

        public string Name { get => _name; set => _name = value; }
        public IEnumerable<SensorModel> Sensors { get => _sensors; set => _sensors = value; }
    }
}
