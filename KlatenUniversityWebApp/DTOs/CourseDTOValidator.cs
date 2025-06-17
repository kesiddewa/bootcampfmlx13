using FluentValidation;

public class CourseDTOValidator : AbstractValidator<CourseDTO>
{
    public CourseDTOValidator()
    {
        RuleFor(c => c.CourseID).NotEmpty().WithMessage("Course ID is required.");
        RuleFor(c => c.CourseTitle).NotEmpty().WithMessage("Course title is required.");
        RuleFor(c => c.CourseCredits).NotEmpty().WithMessage("Course credits are required.");
    }
}
