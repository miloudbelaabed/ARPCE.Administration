using System.Reflection;
using System.Reflection.Emit;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Domain.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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


    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<StudentCourse> StudentCourse { get; set; }
  

    protected override void OnModelCreating(ModelBuilder builder)
    {
       builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
        builder.Entity<Student>()
       .HasMany(e => e.Courses)
       .WithMany(e => e.Students)
       .UsingEntity<StudentCourse>();
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
