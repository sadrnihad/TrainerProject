using webtest.Models;

namespace webtest.Areas.ViewModels.Trainer
{
    public class CreateTrainerVm
    {
        public string Name { get; set; }
        public int SpecialityId { get; set; }
        public IFormFile Image { get; set; }
        public List<Speciality> Specialities { get; set; } = new List<Speciality>();
    }
}
