using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KlatenUniversityWebApp.Controllers;

public class CoursesController : Controller
{
    private readonly SchoolContext _context;

    public CoursesController(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string SearchString)
    {
        if (_context.Courses == null)
        {
            return Problem("Entity set 'SchoolContext.Courses'  is null.");
        }

        var courses = from c in _context.Courses
                       select c;

        if (!String.IsNullOrEmpty(SearchString))
        {
            courses = courses.Where(s => s.Title!.ToUpper().Contains(SearchString.ToUpper()) || s.CourseID.ToString().Contains(SearchString));
        }
        return View(await courses.ToListAsync());
    }

}
