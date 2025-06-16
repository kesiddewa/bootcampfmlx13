using KlatenUniversityWebApp.Repositories;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Data;

namespace KlatenUniversityWebApp.Services
{
    public class StudentsServices : IStudentsServices
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly SchoolContext _context;

        public StudentsServices(IStudentsRepository studentRepository, SchoolContext context)
        {
            _context = context;
            _studentsRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentsRepository.GetAllAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _studentsRepository.GetByIdAsync(id);
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            if (student.EnrollmentDate == default(DateTime))
            {
                student.EnrollmentDate = DateTime.Today;
            }


            await _studentsRepository.AddAsync(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }

            _studentsRepository.UpdateAsync(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentsRepository.GetByIdAsync(id);
            if (student == null)
            {
                return false;
            }

            await _studentsRepository.DeleteAsync(student);
            return await _context.SaveChangesAsync() > 0;
        }        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchString)
        {
            return await _studentsRepository.SearchStudentsAsync(searchString);
        }

        public async Task<Student?> GetStudentWithEnrollmentsAsync(int id)
        {
            return await _studentsRepository.GetStudentWithEnrollmentsAsync(id);
        }

    }

    public interface IStudentsServices
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student> CreateStudentAsync(Student student);
        Task<bool> UpdateStudent(Student student);
        Task<bool> DeleteStudentAsync(int id);
        Task<IEnumerable<Student>> SearchStudentsAsync(string SearchString);
        Task<Student?> GetStudentWithEnrollmentsAsync(int id);
    }
}