using ARPCE.Administration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ARPCE.Administration.Application.Common.Interfaces;
public interface IApplicationDbContext
{


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<Student> Students { get; }
    DbSet<Course> Courses { get; }
}
