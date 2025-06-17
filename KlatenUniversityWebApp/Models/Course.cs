using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace KlatenUniversityWebApp.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.CourseID).NotEmpty().WithMessage("Course ID is required.");
            RuleFor(c => c.Title).NotEmpty().WithMessage("Course title is required.");
            RuleFor(c => c.Credits).NotEmpty().WithMessage("Course credits are required.");
        }
    }

}