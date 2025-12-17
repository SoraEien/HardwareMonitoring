namespace HardwareMonitoring.Models
{
    public class ComputerModel
    {
        private string _name;
        private IEnumerable<SystemModel> _systems;
        private DateTime _time;

        public ComputerModel(string name)
        {
            _name = name;
            _systems = new List<SystemModel>();
            _time = DateTime.Now;
        }

        public string Name { get => _name; set => _name = value; }
        public IEnumerable<SystemModel> Systems { get => _systems; set => _systems = value; }
        public DateTime Time { get => _time; set => _time = value; }
    }
}
