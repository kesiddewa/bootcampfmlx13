using KlatenUniversityWebApp.Repositories;
using KlatenUniversityWebApp.Models;
using KlatenUniversityWebApp.Data;
using AutoMapper;

namespace KlatenUniversityWebApp.Services
{
    public class StudentsServices : IStudentsServices
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly SchoolContext _context;
        private readonly IMapper _mapper;

        public StudentsServices(IStudentsRepository studentRepository, SchoolContext context, IMapper mapper)
        {
            _context = context;
            _studentsRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO?> GetStudentByIdAsync(int id)
        {
            var student = await _studentsRepository.GetByIdAsync(id);
            return student != null ? _mapper.Map<StudentDTO>(student) : null;
        }

        public async Task<StudentDTO> CreateStudentAsync(StudentDTO studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);

            if (student.EnrollmentDate == default(DateTime))
            {
                student.EnrollmentDate = DateTime.Today;
            }

            await _studentsRepository.AddAsync(student);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<bool> UpdateStudent(StudentDTO studentDto)
        {
            if (studentDto == null)
            {
                return false;
            }

            var student = _mapper.Map<Student>(studentDto);
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
        }
        public async Task<IEnumerable<StudentDTO>> SearchStudentsAsync(string searchString)
        {
            var students = await _studentsRepository.SearchStudentsAsync(searchString);
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO?> GetStudentWithEnrollmentsAsync(int id)
        {
            var student = await _studentsRepository.GetStudentWithEnrollmentsAsync(id);
            return student != null ? _mapper.Map<StudentDTO>(student) : null;
        }

    }
    public interface IStudentsServices
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO?> GetStudentByIdAsync(int id);
        Task<StudentDTO> CreateStudentAsync(StudentDTO studentDto);
        Task<bool> UpdateStudent(StudentDTO studentDto);
        Task<bool> DeleteStudentAsync(int id);
        Task<IEnumerable<StudentDTO>> SearchStudentsAsync(string SearchString);
        Task<StudentDTO?> GetStudentWithEnrollmentsAsync(int id);
    }
}