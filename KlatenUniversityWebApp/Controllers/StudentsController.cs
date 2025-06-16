using Microsoft.AspNetCore.Mvc;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Services;

namespace KlatenUniversityWebApp.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentsServices _studentsServices;

    public StudentsController(IStudentsServices studentsServices)
    {
        _studentsServices = studentsServices;
    }
    public async Task<IActionResult> Index(string SearchString)
    {
        var students = await _studentsServices.SearchStudentsAsync(SearchString);

        ViewBag.SearchString = SearchString;

        return View(students);
    }
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _studentsServices.GetStudentWithEnrollmentsAsync(id.Value);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    public IActionResult Create()
    {
        return View(new Student());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Email,PhoneNumber,Address,DateOfBirth,Major,EnrollmentDate")] Student student)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _studentsServices.CreateStudentAsync(student);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating student: {ex.Message}");
            }
        }
        return View(student);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _studentsServices.GetStudentByIdAsync(id.Value);

        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        try
        {
            var success = await _studentsServices.DeleteStudentAsync(id);
            if (!success)
            {
                return NotFound($"Student with ID {id} not found");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error deleting student: {ex.Message}");
            return View(nameof(Index));
        }
    }
}
