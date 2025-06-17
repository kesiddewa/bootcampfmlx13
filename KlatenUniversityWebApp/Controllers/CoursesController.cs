using Microsoft.AspNetCore.Mvc;
using KlatenUniversityWebApp.Services;
using AutoMapper;

namespace KlatenUniversityWebApp.Controllers;

public class CoursesController : Controller
{
    private readonly ICoursesServices _coursesServices;

    private readonly IMapper _mapper;


    public CoursesController(ICoursesServices coursesServices, IMapper mapper)
    {
        _coursesServices = coursesServices;
        _mapper = mapper;

    }

    public async Task<IActionResult> Index(string SearchString)
    {
        var course = await _coursesServices.SearchCoursesAsync(SearchString);

        ViewBag.SearchString = SearchString;

        return View(course);
    }

    public IActionResult Create()
    {
        return View(new CourseDTO());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CourseID,CourseTitle,CourseCredits")] CourseDTO courseDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _coursesServices.CreateCourseAsync(courseDto);
                TempData["SuccessMessage"] = "Course created successfully!";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating course: {ex.Message}");
        }
        
        // If we got to here, something went wrong
        return View(courseDto);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _coursesServices.GetCourseByIdAsync(id.Value);

        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        try
        {
            var success = await _coursesServices.DeleteCourseAsync(id);
            if (!success)
            {
                return NotFound($"Course with ID {id} not found");
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error deleting course: {ex.Message}");
            return View(nameof(Index));
        }
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var courseDto = await _coursesServices.GetCourseByIdAsync(id.Value);
        if (courseDto == null)
        {
            return NotFound();
        }

        return View(courseDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CourseID,CourseTitle,CourseCredits")] CourseDTO courseDto)
    {
        if (id != courseDto.CourseID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var success = await _coursesServices.UpdateCourse(courseDto);
                if (!success)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating course: {ex.Message}");
            }
        }
        return View(courseDto);
    }

}
