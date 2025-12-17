namespace HardwareMonitoring.Models
{
    public class SystemModel
    {
        private string _name;
        private IEnumerable<SensorModel> _sensors;
        private DateTime _time;

        public string Name { get => _name; set => _name = value; }
        public IEnumerable<SensorModel> Sensors { get => _sensors; set => _sensors = value; }
        public DateTime Time { get => _time; set => _time = value; }
    }
}
