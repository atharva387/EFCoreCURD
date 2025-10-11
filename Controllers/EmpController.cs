using System.Linq;
using EFCoreCURD.Data;               // <-- your project name
using EFCoreCURD.Models;             // <-- your project name
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // for EntityState and AsNoTracking

namespace EFCoreCURD.Controllers      // <-- your project name
{
    public class EmpController : Controller
    {
        private readonly CompanyDbContext _db;
        public EmpController(CompanyDbContext db) => _db = db;

        public IActionResult Index()
        {
            var emps = _db.Employees.AsNoTracking().ToList();
            return View(emps);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (!ModelState.IsValid) return View(emp);
            _db.Employees.Add(emp);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var emp = _db.Employees.Find(id);
            return emp is null ? NotFound() : View(emp);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Employee emp)
        {
            if (!ModelState.IsValid) return View(emp);
            _db.Entry(emp).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var emp = _db.Employees.Find(id);
            return emp is null ? NotFound() : View(emp);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var emp = _db.Employees.Find(id);
            if (emp is null) return NotFound();
            _db.Employees.Remove(emp);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}