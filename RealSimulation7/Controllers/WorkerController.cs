using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealSimulation7.DAL;
using RealSimulation7.Models;

namespace RealSimulation7.Controllers
{
    public class WorkerController : Controller
    {
        public readonly AppDbContext _context;

        public WorkerController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Worker> workers = await _context.Workers.ToListAsync();
            return View(workers);
        }
    }
}
