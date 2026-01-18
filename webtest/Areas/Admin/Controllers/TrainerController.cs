using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using webtest.Areas.ViewModels.Trainer;
using webtest.DAL.Context;
using webtest.Models;
using webtest.Utilities.Enums;
using webtest.Utilities.Extensions;

namespace webtest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TrainerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Trainer> trainers = await _context.Trainers.Include(w => w.Speciality).ToListAsync();

            return View(trainers);
        }

        public async Task<IActionResult> Create()
        {
            List<Speciality> specialities = await _context.Specialities.ToListAsync();

            CreateTrainerVm createTrainerVm = new()
            {
                Specialities = specialities
            };

            return View(createTrainerVm);
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateTrainerVm createTrainerVm)
        {
            if (!ModelState.IsValid)
            {
                createTrainerVm.Specialities = await _context.Specialities.ToListAsync();
                return View(createTrainerVm);
            }


            Trainer trainer = new()
            {
                Name = createTrainerVm.Name,
                SpecialityId = createTrainerVm.SpecialityId,
                Image = await createTrainerVm.Image.CreateFileAsync()
            };
            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }

        public async Task<IActionResult> Edit(int id)
        {
            Trainer? trainer = await _context.Trainers.FirstOrDefaultAsync(w => w.Id == id);
            if(trainer == null)
            {
                return NotFound();
            }

            EditTrainerVm editTrainerVm = new()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                SpecialityId = trainer.SpecialityId,
                Specialities = await _context.Specialities.ToListAsync()

            };

            return View(editTrainerVm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTrainerVm editTrainerVm)
        {
            if (!ModelState.IsValid)
            {
                editTrainerVm.Specialities = await _context.Specialities.ToListAsync();
                return View(editTrainerVm);
            }


            Trainer? trainer = await _context.Trainers.FirstOrDefaultAsync(w => w.Id == id);
            if (trainer == null)
            {
                return NotFound(); 
            }

            if (await _context.Trainers.AnyAsync(w => w.Name == editTrainerVm.Name && w.Id != id))
            {
                ModelState.AddModelError("Name", "This trainer name is already taken.");
                editTrainerVm.Specialities = await _context.Specialities.ToListAsync();
                return View(editTrainerVm);
            }

            trainer.Name = editTrainerVm.Name;
            trainer.SpecialityId = editTrainerVm.SpecialityId;

            if (editTrainerVm.Image != null)
            {
                if (!editTrainerVm.Image.ValidateType("image"))
                {
                    ModelState.AddModelError("Image", "Please select a valid image file.");
                    editTrainerVm.Specialities = await _context.Specialities.ToListAsync();
                    return View(editTrainerVm);
                }

                if (!editTrainerVm.Image.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB.");
                    editTrainerVm.Specialities = await _context.Specialities.ToListAsync();
                    return View(editTrainerVm);
                }


                trainer.Image = await editTrainerVm.Image.CreateFileAsync(_env.WebRootPath, "assets", "img");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
