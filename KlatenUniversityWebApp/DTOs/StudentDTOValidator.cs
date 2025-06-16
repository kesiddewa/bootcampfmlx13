using FluentValidation;

public class StudentDTOValidator : AbstractValidator<StudentDTO>
{
    public StudentDTOValidator()
    {
        RuleFor(s => s.StudentName).NotEmpty().WithMessage("Student name is required.");
        RuleFor(s => s.Email).EmailAddress().WithMessage("Invalid email format.");
        RuleFor(s => s.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^[\d\-\+\(\)\s]+$").WithMessage("Phone number contains invalid characters.");
        RuleFor(s => s.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(s => s.DateOfBirth).LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
        RuleFor(s => s.StudentMajor).NotEmpty().WithMessage("Major is required.");
        RuleFor(s => s.EnrollmentDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Enrollment date cannot be in the future.");
    }
}
