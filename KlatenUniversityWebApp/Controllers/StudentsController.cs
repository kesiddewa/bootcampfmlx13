using Microsoft.AspNetCore.Mvc;
using KlatenUniversityWebApp.Data;
using Microsoft.EntityFrameworkCore;
using KlatenUniversityWebApp.Models;

namespace KlatenUniversityWebApp.Controllers;

public class StudentsController : Controller
{
    private readonly SchoolContext _context;

    public StudentsController(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string SearchString)
    {
        if (_context.Students == null)
        {
            return Problem("Entity set 'SchoolContext.Students'  is null.");
        }

        var students = from s in _context.Students
                       select s;

        if (!String.IsNullOrEmpty(SearchString))
        {
            students = students.Where(s => s.Name!.ToUpper().Contains(SearchString.ToUpper()) || s.Major.ToUpper().Contains(SearchString.ToUpper()));
        }
        return View(await students.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Students == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        return View(student);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name,Email,PhoneNumber,Address,DateOfBirth,Major,EnrollmentDate")] Student student)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        catch (DbUpdateException /* ex */)
        {
            //Log the error (uncomment ex variable name and write a log.
            ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
        }
        return View(student);
    }
}
