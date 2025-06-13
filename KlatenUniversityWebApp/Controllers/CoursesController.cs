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

    public async Task<IActionResult> Index()
    {
        return View(await _context.Courses.ToListAsync());
    }

}
