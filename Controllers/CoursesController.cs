using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearningMVC.Data;
using OnlineLearningMVC.Models;

namespace OnlineLearningMVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Courses.Include(c => c.CategoryId).ToListAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(new Course());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", model.CategoryId);
                return View(model);
            }
            _context.Courses.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Курс створено";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _context.Courses.FindAsync(id);
            if (c == null) return NotFound();
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", c.CategoryId);
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", model.CategoryId);
                return View(model);
            }
            _context.Courses.Update(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Змінено";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var c = await _context.Courses.FindAsync(id);
            if (c == null) return NotFound();
            return PartialView("_DeleteConfirmPartial", Tuple.Create(id, Url.Action("Delete", new { id }), c.Title));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var c = await _context.Courses.FindAsync(id);
            if (c == null) return NotFound();
            _context.Courses.Remove(c);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Курс видалено";
            return RedirectToAction(nameof(Index));
        }
    }
}
