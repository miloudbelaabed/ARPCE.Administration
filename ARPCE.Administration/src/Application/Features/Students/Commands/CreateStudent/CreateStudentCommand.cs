using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using MediatR;

namespace ARPCE.Administration.Application.Features.StudentFeatures.Commands.CreateStudent;
public class CreateStudentCommand:IRequest<Guid>
{
    public string Name { get; set; }
}


public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentService _studentService;

    public CreateStudentCommandHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var entity = new StudentVm
        {
            Name = request.Name,
        };
        Guid id = await _studentService.CreateStudentAsync(entity);



        return id;
    }
}
