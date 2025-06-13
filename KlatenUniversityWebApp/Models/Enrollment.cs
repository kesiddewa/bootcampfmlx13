namespace KlatenUniversityWebApp.Models
{
    public enum Grade {
        A, B, C, D, E, K
    }
    public class Enrollment
    {
        public Guid EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }

    }
}