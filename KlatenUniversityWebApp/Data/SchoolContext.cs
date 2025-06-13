using Microsoft.EntityFrameworkCore;
using KlatenUniversityWebApp.Models;

namespace KlatenUniversityWebApp.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(s => s.StudentID);

            modelBuilder.Entity<Course>()
                .HasMany(s => s.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(s => s.CourseID);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentID);

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student 
                {
                    ID = 1,
                    Name = "Carson Alexander", 
                    Email = "carson.alexander@klaten.edu", 
                    PhoneNumber = "081234567890", 
                    Address = "Jl. Sudirman No. 123, Klaten", 
                    DateOfBirth = DateTime.Parse("1987-05-15"), 
                    Major = "Computer Science", 
                    EnrollmentDate = DateTime.Parse("2005-09-01") 
                },
                new Student 
                { 
                    ID = 2,
                    Name = "Meredith Alonso", 
                    Email = "meredith.alonso@klaten.edu", 
                    PhoneNumber = "081234567891", 
                    Address = "Jl. Ahmad Yani No. 45, Klaten", 
                    DateOfBirth = DateTime.Parse("1984-03-22"), 
                    Major = "Mathematics", 
                    EnrollmentDate = DateTime.Parse("2002-09-01") 
                },
                new Student 
                { 
                    ID = 3,
                    Name = "Arturo Anand", 
                    Email = "arturo.anand@klaten.edu", 
                    PhoneNumber = "081234567892", 
                    Address = "Jl. Diponegoro No. 67, Klaten", 
                    DateOfBirth = DateTime.Parse("1985-07-10"), 
                    Major = "Physics", 
                    EnrollmentDate = DateTime.Parse("2003-09-01") 
                },
                new Student 
                { 
                    ID = 4,
                    Name = "Gytis Barzdukas", 
                    Email = "gytis.barzdukas@klaten.edu", 
                    PhoneNumber = "081234567893", 
                    Address = "Jl. Gajah Mada No. 89, Klaten", 
                    DateOfBirth = DateTime.Parse("1984-11-03"), 
                    Major = "Chemistry", 
                    EnrollmentDate = DateTime.Parse("2002-09-01") 
                },
                new Student 
                { 
                    ID = 5,
                    Name = "Yan Li", 
                    Email = "yan.li@klaten.edu", 
                    PhoneNumber = "081234567894", 
                    Address = "Jl. Pemuda No. 12, Klaten", 
                    DateOfBirth = DateTime.Parse("1984-09-18"), 
                    Major = "Engineering", 
                    EnrollmentDate = DateTime.Parse("2002-09-01") 
                },
                new Student 
                { 
                    ID = 6,
                    Name = "Peggy Justice", 
                    Email = "peggy.justice@klaten.edu", 
                    PhoneNumber = "081234567895", 
                    Address = "Jl. Veteran No. 34, Klaten", 
                    DateOfBirth = DateTime.Parse("1983-01-25"), 
                    Major = "Biology", 
                    EnrollmentDate = DateTime.Parse("2001-09-01") 
                },
                new Student 
                { 
                    ID = 7,
                    Name = "Laura Norman", 
                    Email = "laura.norman@klaten.edu", 
                    PhoneNumber = "081234567896", 
                    Address = "Jl. Kartini No. 56, Klaten", 
                    DateOfBirth = DateTime.Parse("1985-12-08"), 
                    Major = "Psychology", 
                    EnrollmentDate = DateTime.Parse("2003-09-01") 
                },
                new Student 
                { 
                    ID = 8,
                    Name = "Nino Olivetto", 
                    Email = "nino.olivetto@klaten.edu", 
                    PhoneNumber = "081234567897", 
                    Address = "Jl. Pahlawan No. 78, Klaten", 
                    DateOfBirth = DateTime.Parse("1987-04-14"), 
                    Major = "Economics", 
                    EnrollmentDate = DateTime.Parse("2005-09-01") 
                }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseID = 1050, Title = "Chemistry", Credits = 3 },
                new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3 },
                new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3 },
                new Course { CourseID = 1045, Title = "Calculus", Credits = 4 },
                new Course { CourseID = 3141, Title = "Trigonometry", Credits = 4 },
                new Course { CourseID = 2021, Title = "Composition", Credits = 3 },
                new Course { CourseID = 2042, Title = "Literature", Credits = 4 },
                new Course { CourseID = 1001, Title = "Introduction to Computer Science", Credits = 3 },
                new Course { CourseID = 2001, Title = "Data Structures", Credits = 4 },
                new Course { CourseID = 3001, Title = "Database Systems", Credits = 3 }
            );

            // Seed Enrollments
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 1, CourseID = 1050, Grade = Grade.A },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 1, CourseID = 4022, Grade = Grade.C },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 1, CourseID = 4041, Grade = Grade.B },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 2, CourseID = 1045, Grade = Grade.B },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 2, CourseID = 3141, Grade = Grade.K },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 2, CourseID = 2021, Grade = Grade.C },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 3, CourseID = 1050 },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 4, CourseID = 1050 },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 4, CourseID = 4022, Grade = Grade.K },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 5, CourseID = 4041, Grade = Grade.C },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 6, CourseID = 1045 },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 7, CourseID = 3141, Grade = Grade.A },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 1, CourseID = 1001, Grade = Grade.A },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 1, CourseID = 2001, Grade = Grade.B },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 3, CourseID = 1001, Grade = Grade.B },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 5, CourseID = 2001 },
                new Enrollment { EnrollmentID = Guid.NewGuid(), StudentID = 8, CourseID = 3001, Grade = Grade.A }
            );
        }
    }
}