using System.Reflection;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Domain.Entities;
using ARPCE.Administration.Infrastructure.Identity;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ARPCE.Administration.Infrastructure.Persistence;
public class ARPCEDbContext : ApiAuthorizationDbContext<ARPCEUser>, IApplicationDbContext
/*IdentityDbContext<ARPCEUser>*/
{
    private readonly IMediator _mediator;


    public ARPCEDbContext(
        DbContextOptions<ARPCEDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;

    }


    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();
  

    protected override void OnModelCreating(ModelBuilder builder)
    {
       builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
