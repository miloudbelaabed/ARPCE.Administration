using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using MediatR;

namespace ARPCE.Administration.Application.Features.Students.Commands.DeleteStudent;
public class DeleteStudentCommand:IRequest<Result>
{
    public Guid Id { get; set; }
}
public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Result>
{
    private readonly IStudentService _studentService;
     public DeleteStudentCommandHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }
    public Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {

        return _studentService.DeleteAsync(request.Id);
    }
}
