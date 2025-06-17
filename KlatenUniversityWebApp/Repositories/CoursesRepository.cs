using KlatenUniversityWebApp.Data;
using KlatenUniversityWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KlatenUniversityWebApp.Repositories
{

    public class CoursesRepository : GenericRepository<Course>, ICoursesRepository
    {
        public CoursesRepository(SchoolContext context) : base(context)
        {
        }

        public async Task<Course?> FindCourseByIdAsync(int courseId)
        {
            return await _dbSet.FindAsync(courseId);
        }

        public async Task<IEnumerable<Course>> SearchCoursesAsync(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await GetAllAsync();
            }

            string lowerSearchString = searchString.ToLower();

            return await _dbSet
                .Where(c => c.Title.ToLower().Contains(lowerSearchString) ||
                            c.CourseID.ToString().ToLower().Contains(lowerSearchString))
                .OrderBy(c => c.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> FindCourseByTitle(string courseTitle)
        {
            if (string.IsNullOrWhiteSpace(courseTitle))
            {
                return new List<Course>();
            }

            return await _dbSet.Where(c => c.Title.ToLower().Contains(courseTitle.ToLower())).ToListAsync();
        }

        public async Task<Course?> GetCourseWithEnrollmentsAsync(int courseId)
        {
            return await _dbSet
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(c => c.CourseID == courseId);
        }

        public Task<IEnumerable<Course>> FindCourseByTitleAsync(string CourseTitle)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICoursesRepository : IGenericRepository<Course>
    {
        Task<Course?> FindCourseByIdAsync(int courseId);
        Task<IEnumerable<Course>> SearchCoursesAsync(string searchString);
        Task<IEnumerable<Course>> FindCourseByTitleAsync(string CourseTitle);
        Task<Course?> GetCourseWithEnrollmentsAsync(int courseId);
    }
}