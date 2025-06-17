using KlatenUniversityWebApp.Repositories;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Data;
using AutoMapper;

namespace KlatenUniversityWebApp.Services
{
    public class CoursesServices : ICoursesServices
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public CoursesServices(ICoursesRepository coursesRepository, SchoolContext context, IMapper mapper)
        {
            _context = context;
            _coursesRepository = coursesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            var courses = await _coursesRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }
        public async Task<CourseDTO?> GetCourseByIdAsync(int id)
        {
            var course = await _coursesRepository.FindCourseByIdAsync(id);
            return course != null ? _mapper.Map<CourseDTO>(course) : null;
        }
        public async Task<CourseDTO> CreateCourseAsync(CourseDTO courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _coursesRepository.AddAsync(course);
            await _context.SaveChangesAsync();
            return _mapper.Map<CourseDTO>(course);
        }
        public async Task<bool> UpdateCourse(CourseDTO courseDto)
        {
            if (courseDto == null)
            {
                return false;
            }

            var course = _mapper.Map<Course>(courseDto);
            _coursesRepository.UpdateAsync(course);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _coursesRepository.FindCourseByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            await _coursesRepository.DeleteAsync(course);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<CourseDTO>> SearchCoursesAsync(string searchString)
        {
            var courses = await _coursesRepository.SearchCoursesAsync(searchString);
            return _mapper.Map<IEnumerable<CourseDTO>>(courses);
        }

        public async Task<CourseDTO?> GetCourseWithEnrollmentsAsync(int id)
        {
            var course = await _coursesRepository.GetCourseWithEnrollmentsAsync(id);
            return course != null ? _mapper.Map<CourseDTO>(course) : null;
        }
    }
    public interface ICoursesServices
    {
        Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        Task<CourseDTO?> GetCourseByIdAsync(int id);
        Task<CourseDTO> CreateCourseAsync(CourseDTO courseDto);
        Task<bool> UpdateCourse(CourseDTO courseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task<IEnumerable<CourseDTO>> SearchCoursesAsync(string SearchString);
        Task<CourseDTO?> GetCourseWithEnrollmentsAsync(int id);
    }
}