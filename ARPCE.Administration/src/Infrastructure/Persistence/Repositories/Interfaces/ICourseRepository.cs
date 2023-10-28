using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Features.Courses.Commandes;
using Azure.Core;

namespace ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
public interface ICourseRepository
{
    Task<Guid> CreateCourseAsync(CreateCourseCommand request);
}
