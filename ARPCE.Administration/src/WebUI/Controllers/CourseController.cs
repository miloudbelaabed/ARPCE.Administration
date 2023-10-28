using ARPCE.Administration.Application.Features.Courses.Commandes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARPCE.Administration.WebUI.Controllers;

public class CourseController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCourseCommand command)
    {
        return await Mediator.Send(command);
    }
}
