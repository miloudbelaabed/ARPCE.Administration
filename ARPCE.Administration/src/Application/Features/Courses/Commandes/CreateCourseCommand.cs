using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using MediatR;

namespace ARPCE.Administration.Application.Features.Courses.Commandes;
public class CreateCourseCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseService _courseService;
    public CreateCourseCommandHandler(ICourseService courseService)
    {
        _courseService = courseService;
    }
    public Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
       return _courseService.CreateCourseAsync(request);
    }
}