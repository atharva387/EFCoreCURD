using System.Linq;
using EFCoreCURD.Data;
using EFCoreCURD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFCoreCURD.Controllers
{
    public class EmpController : Controller
    {
        private readonly CompanyDbContext _db;
        public EmpController(CompanyDbContext db) => _db = db;

        public IActionResult Index()
        {
            var emps = _db.Employees.AsNoTracking().ToList();
            ViewBag.Departments = _db.Departments.AsNoTracking().ToList();
            return View(emps);
        }

        // ---------- CREATE ----------
        // GET
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
                _db.Departments.AsNoTracking().OrderBy(d => d.DepartmentName).ToList(),
                "DepartmentName",   // value
                "DepartmentName"    // text
            );
            return View();
        }

        // POST
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                // re-populate dropdown when returning the view
                ViewBag.Departments = new SelectList(
                    _db.Departments.AsNoTracking().OrderBy(d => d.DepartmentName).ToList(),
                    "DepartmentName", "DepartmentName", emp.DepartmentName
                );
                return View(emp);
            }

            _db.Employees.Add(emp);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // ---------- EDIT ----------
        // GET
        public IActionResult Edit(int id)
        {
            var emp = _db.Employees.Find(id);
            if (emp is null) return NotFound();

            ViewBag.Departments = new SelectList(
                _db.Departments.AsNoTracking().OrderBy(d => d.DepartmentName).ToList(),
                "DepartmentName", "DepartmentName", emp.DepartmentName
            );
            return View(emp);
        }

        // POST
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(
                    _db.Departments.AsNoTracking().OrderBy(d => d.DepartmentName).ToList(),
                    "DepartmentName", "DepartmentName", emp.DepartmentName
                );
                return View(emp);
            }

            _db.Entry(emp).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // ---------- DELETE ----------
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
