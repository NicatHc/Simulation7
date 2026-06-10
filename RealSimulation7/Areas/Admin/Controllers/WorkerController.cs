using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealSimulation7.DAL;
using RealSimulation7.Models;
using RealSimulation7.Utilities.Enums;
using RealSimulation7.Utilities.Extentions;
using RealSimulation7.ViewModels.Worker;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace RealSimulation7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorkerController : Controller
    {
        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public WorkerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Worker> workers = await _context.Workers.ToListAsync();
            return View(workers);
        }





        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM createVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(!createVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Type ERROR");
                return View();
            }

            if (!createVM.Photo.ValidateSize(3, FileSize.MB))
            {
                ModelState.AddModelError("Photo", "Size ERROR");
                return View();
            }

            Worker worker = new Worker
            {
                Name = createVM.Name,
                Job = createVM.Job,
                Image = await createVM.Photo.CreateFile(_env.WebRootPath, "assets", "img", "team"),
            };
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Worker worker = await _context.Workers.FirstOrDefaultAsync(worker => worker.Id == id);

            if (worker == null) return NotFound();
            return View(worker);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Worker worker = await _context.Workers.FirstOrDefaultAsync(worker => worker.Id == id);

            if (worker == null) return NotFound();

            worker.Image.DeleteFile(_env.WebRootPath, "assets", "img", "team");
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Worker worker = await _context.Workers.FirstOrDefaultAsync(worker => worker.Id == id);

            if (worker == null) return NotFound();

            UpdateVM updateVM = new UpdateVM()
            {
                Name = worker.Name,
                Job = worker.Job,
                Image = worker.Image,
            };

            return View(updateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateVM updateVM)
        {
            if (id == null || id < 1) return BadRequest();

            Worker worker = await _context.Workers.FirstOrDefaultAsync(worker => worker.Id == id);

            if (worker == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(updateVM);
            }

            if(updateVM.Photo != null)
            {
                if (!updateVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "Type ERROR");
                    return View();
                }

                if (!updateVM.Photo.ValidateSize(3, FileSize.MB))
                {
                    ModelState.AddModelError("Photo", "Size ERROR");
                    return View();
                }

                worker.Image.DeleteFile(_env.WebRootPath, "assets", "img", "team");
                worker.Image = await updateVM.Photo.CreateFile(_env.WebRootPath, "assets", "img", "team");
            }

            worker.Name=updateVM.Name;
            worker.Job=updateVM.Job;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
