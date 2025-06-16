using FluentValidation;

namespace KlatenUniversityWebApp.Models
{    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Major { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(s => s.Email).EmailAddress().WithMessage("Invalid email format.");
            RuleFor(s => s.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[\d\-\+\(\)\s]+$").WithMessage("Phone number contains invalid characters.");
            RuleFor(s => s.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(s => s.DateOfBirth).LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
            RuleFor(s => s.Major).NotEmpty().WithMessage("Major is required.");
            RuleFor(s => s.EnrollmentDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Enrollment date cannot be in the future.");
        }
    }
}