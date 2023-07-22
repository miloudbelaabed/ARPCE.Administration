using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Application.Features.StudentFeatures.Commands.CreateStudent;
using ARPCE.Administration.Application.Features.StudentFeatures.Queries.GetStudents;
using ARPCE.Administration.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ARPCE.Administration.WebUI.Controllers;
public class StudentController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<StudentVm>>> Get([FromQuery] GetStudentsQuery query)
    {
        return await Mediator.Send(query);
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateStudentCommand command)
    {
        return await Mediator.Send(command);
    }
}
