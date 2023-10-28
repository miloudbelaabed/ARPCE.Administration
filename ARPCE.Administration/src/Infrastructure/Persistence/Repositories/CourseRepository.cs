using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Features.Courses.Commandes;
using ARPCE.Administration.Domain.Entities;
using ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
using AutoMapper;

namespace ARPCE.Administration.Infrastructure.Persistence.Repositories;
public class CourseRepository : ICourseRepository
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CourseRepository(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Guid> CreateCourseAsync(CreateCourseCommand request)
    {
        var entity = _mapper.Map<Course>(request);
        await _context.Courses.AddAsync(entity);
        return entity.Id;
    }
}
