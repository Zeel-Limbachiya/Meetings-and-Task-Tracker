using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingAndTaskTracker.Models;
using MeetingAndTaskTracker.ViewModels;

namespace MeetingAndTaskTracker.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MeetTrackDbContext _context;

        public EmployeeController(MeetTrackDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Employees()
        {
            var meetTrackDbContext = _context.Employees.Include(e => e.Department);
            return View(await meetTrackDbContext.ToListAsync());
        }
        public IActionResult AddEmployee()
        {
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    DepartmentId = model.DepartmentId
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Employees));
            }
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", employee.DepartmentId);
            return View(model);
        }

        public async Task<IActionResult> EmployeeDetails(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        public async Task<IActionResult> EditEmployee(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(int id, EmployeeViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = id,
                    Name = model.Name,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    DepartmentId = model.DepartmentId
                };
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Employees));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", model.DepartmentId);
            return View(model);
        }

        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployeeConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
                _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Employees));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
