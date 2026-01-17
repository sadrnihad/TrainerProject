using webtest.Models.Common;

namespace webtest.Models
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
        
    }
}
