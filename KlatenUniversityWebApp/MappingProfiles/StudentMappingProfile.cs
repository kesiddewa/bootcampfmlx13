using AutoMapper;
using KlatenUniversityWebApp.Models;

namespace KlatenUniversityWebApp.MappingProfiles
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StudentMajor, opt => opt.MapFrom(src => src.Major));

            CreateMap<StudentDTO, Student>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StudentName))
                .ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.StudentMajor));
        }
    }
}