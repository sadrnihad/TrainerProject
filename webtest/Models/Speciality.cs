using webtest.Models.Common;

namespace webtest.Models
{
    public class Speciality : BaseEntity
    {
        public string Name { get; set; }
        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
    }
}
