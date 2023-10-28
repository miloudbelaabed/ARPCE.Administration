using System.Reflection;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Application.Features.Courses.Commandes;
using ARPCE.Administration.Application.Features.StudentFeatures.Commands.CreateStudent;
using ARPCE.Administration.Domain.Entities;
using AutoMapper;

namespace ARPCE.Administration.Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseVm>().ReverseMap();
        CreateMap<Course, CreateCourseCommand>().ReverseMap();
        CreateMap<Student, StudentVm>().ForMember(dest => dest.courses,opt => opt.MapFrom(src => src.Courses));

        CreateMap<Student, CreateStudentCommand>().ReverseMap();

        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
