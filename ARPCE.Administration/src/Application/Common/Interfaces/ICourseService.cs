using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Features.Courses.Commandes;

namespace ARPCE.Administration.Application.Common.Interfaces;
public interface ICourseService
{
   Task<Guid> CreateCourseAsync(CreateCourseCommand request);
}
