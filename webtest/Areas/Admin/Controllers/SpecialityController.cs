using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webtest.DAL.Context;
using webtest.Models;

namespace webtest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialityController : Controller
    {
        private readonly AppDbContext _context;

        public SpecialityController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Speciality> specialities = await _context.Specialities.ToListAsync();
            return View(specialities);
        }
    }
}
