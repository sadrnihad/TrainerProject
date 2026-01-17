using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webtest.DAL.Context;
using webtest.Models;

namespace webtest.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
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
