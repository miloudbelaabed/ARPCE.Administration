using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Features.Courses.Commandes;
using ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;

namespace ARPCE.Administration.Infrastructure.Services;
public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    public CourseService(ICourseRepository courseRepository) {
    _courseRepository = courseRepository;
    }
    public Task<Guid> CreateCourseAsync(CreateCourseCommand request)
    {
       return _courseRepository.CreateCourseAsync(request);
    }
}
