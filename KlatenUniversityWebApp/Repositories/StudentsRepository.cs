using KlatenUniversityWebApp.Data;
using KlatenUniversityWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KlatenUniversityWebApp.Repositories
{
    public class StudentsRepository : GenericRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(SchoolContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Student>> GetStudentsByMajorAsync(string major)
        {
            return await _dbSet.Where(s => s.Major == major).OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByEnrollmentDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(s => s.EnrollmentDate >= startDate && s.EnrollmentDate <= endDate)
                .OrderBy(s => s.EnrollmentDate)
                .ThenBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> FindStudentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<Student>();
            }

            return await _dbSet.Where(s => s.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await GetAllAsync();
            }

            string lowerSearchString = searchString.ToLower();

            return await _dbSet
                .Where(s => s.Name.ToLower().Contains(lowerSearchString) ||
                            s.Email.ToLower().Contains(lowerSearchString) ||
                            s.Major.ToLower().Contains(lowerSearchString))                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<Student?> GetStudentWithEnrollmentsAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.ID == id);
        }
    }
    public interface IStudentsRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsByMajorAsync(string major);
        Task<IEnumerable<Student>> GetStudentsByEnrollmentDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Student>> FindStudentByName(string name);
        Task<IEnumerable<Student>> SearchStudentsAsync(string searchString);
        Task<Student?> GetStudentWithEnrollmentsAsync(int id);
    }
}