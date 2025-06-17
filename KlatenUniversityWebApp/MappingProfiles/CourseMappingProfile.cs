using AutoMapper;
using KlatenUniversityWebApp.Models;

namespace KlatenUniversityWebApp.MappingProfiles
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.CourseCredits, opt => opt.MapFrom(src => src.Credits));

            CreateMap<CourseDTO, Course>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.CourseTitle))
                .ForMember(dest => dest.Credits, opt => opt.MapFrom(src => src.CourseCredits));
        }
    }
}