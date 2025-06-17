using KlatenUniversityWebApp.Models;
using System.ComponentModel.DataAnnotations;

public class CourseDTO
{
    [Required(ErrorMessage = "Course ID is required")]
    [Display(Name = "Course ID")]
    public int CourseID { get; set; }
    
    [Required(ErrorMessage = "Course title is required")]
    [Display(Name = "Course Title")]
    public string CourseTitle { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Course credits are required")]
    [Display(Name = "Credits")]
    public string CourseCredits { get; set; } = string.Empty;
    
    public ICollection<Enrollment>? Enrollments { get; set; }
}