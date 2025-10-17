using EFCoreCURD.Data;
using EFCoreCURD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCURD.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly CompanyDbContext _db;
        public DepartmentController(CompanyDbContext db) => _db = db;

        // READ: list
        public IActionResult Index()
        {
            var list = _db.Departments.AsNoTracking().ToList();
            return View(list);
        }

        // CREATE: form
        [HttpGet]
        public IActionResult Create() => View();

        // CREATE: submit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Department dept)
        {
            if (!ModelState.IsValid) return View(dept);
            _db.Departments.Add(dept);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // EDIT: form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _db.Departments.Find(id);
            return dept is null ? NotFound() : View(dept);
        }

        // EDIT: submit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Department dept)
        {
            if (!ModelState.IsValid) return View(dept);
            _db.Entry(dept).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // DELETE: confirm page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dept = _db.Departments.Find(id);
            return dept is null ? NotFound() : View(dept);
        }

        // DELETE: submit
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dept = _db.Departments.Find(id);
            if (dept is null) return NotFound();
            _db.Departments.Remove(dept);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // OPTIONAL: details page
        [HttpGet]
        public IActionResult Details(int id)
        {
            var dept = _db.Departments.AsNoTracking().FirstOrDefault(d => d.DepartmentId == id);
            return dept is null ? NotFound() : View(dept);
        }
    }
}
