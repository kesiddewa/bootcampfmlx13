using Microsoft.AspNetCore.Mvc;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Services;
using AutoMapper;

namespace KlatenUniversityWebApp.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentsServices _studentsServices;
    private readonly IMapper _mapper;

    public StudentsController(IStudentsServices studentsServices, IMapper mapper)
    {
        _studentsServices = studentsServices;
        _mapper = mapper;
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
    }    public IActionResult Create()
    {
        return View(new StudentDTO());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("StudentName,Email,PhoneNumber,Address,DateOfBirth,StudentMajor,EnrollmentDate")] StudentDTO studentDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _studentsServices.CreateStudentAsync(studentDto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating student: {ex.Message}");
            }
        }
        return View(studentDto);
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
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studentDto = await _studentsServices.GetStudentByIdAsync(id.Value);
        if (studentDto == null)
        {
            return NotFound();
        }

        return View(studentDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,StudentName,Email,PhoneNumber,Address,DateOfBirth,StudentMajor,EnrollmentDate")] StudentDTO studentDto)
    {
        if (id != studentDto.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var success = await _studentsServices.UpdateStudent(studentDto);
                if (!success)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating student: {ex.Message}");
            }
        }
        return View(studentDto);
    }
}
