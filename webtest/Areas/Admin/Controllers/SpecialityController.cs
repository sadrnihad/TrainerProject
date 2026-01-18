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

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Speciality speciality)
        {
            if (!ModelState.IsValid)
            {
                return View(speciality);
            }
            await _context.Specialities.AddAsync(speciality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            Speciality? speciality = await _context.Specialities.FirstOrDefaultAsync(d => d.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }
            return View(speciality);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Speciality speciality)
        {
            if (!ModelState.IsValid)
            {
                return View(speciality);
            }

            Speciality? dbSpeciality = await _context.Specialities.FirstOrDefaultAsync(d => d.Id == speciality.Id);

            if (dbSpeciality == null)
            {
                return NotFound();
            }

            if (await _context.Specialities.AnyAsync(d => d.Name == speciality.Name && d.Id != dbSpeciality.Id))
            {
                ModelState.AddModelError("Name", "This name is already taken.");
                return View(speciality);
            }

            dbSpeciality.Name = speciality.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            Speciality? speciality = await _context.Specialities.FirstOrDefaultAsync(d => d.Id == id);
            if(speciality == null)
            {
                return NotFound();
            }

            _context.Specialities.Remove(speciality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
