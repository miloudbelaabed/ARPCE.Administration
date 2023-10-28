
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Domain.Entities;
using MediatR;

namespace ARPCE.Administration.Application.Features.StudentFeatures.Queries.GetStudents;
public class GetStudentsQuery : IRequest<List<StudentVm>>
{
}
public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, List<StudentVm>>
{
    private readonly IStudentService _studentService;
    public GetStudentsQueryHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }
    public async Task<List<StudentVm>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)

    {
        return await _studentService.GetAllStudentsAsync();
    }
}
