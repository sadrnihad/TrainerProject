using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webtest.DAL.Context;
using webtest.Models;

namespace webtest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _context;

        public TrainerController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Trainer> trainers = await _context.Trainers.Include(w => w.Speciality).ToListAsync();

            return View(trainers);
        }
    }
}
