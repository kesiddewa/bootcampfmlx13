using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KlatenUniversityWebApp.Controllers;

public class StudentsController : Controller
{
    private readonly SchoolContext _context;

    public StudentsController(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Students.ToListAsync());
    }

    public async Task<IActionResult> Search(string search)
    {
        if (_context.Students == null)
        {
            return Problem("Entity set 'SchoolContext.Students'  is null.");
        }

        var students = from s in _context.Students
                       select s;

        if (String.IsNullOrEmpty(search))
        {
            students = students.Where(s => s.Name!.ToUpper().Contains(search.ToUpper()));
        }
        return View(await students.ToListAsync());
    }

}
