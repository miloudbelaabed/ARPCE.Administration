using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Infrastructure.Files;
using ARPCE.Administration.Infrastructure.Identity;
using ARPCE.Administration.Infrastructure.Persistence;
using ARPCE.Administration.Infrastructure.Persistence.Repositories;
using ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
using ARPCE.Administration.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ARPCEDbContext>(options =>
                options.UseInMemoryDatabase("ARPCE.AdministrationDb"));
        }
        else
        {
            services.AddDbContext<ARPCEDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ARPCEDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => (IApplicationDbContext)provider.GetRequiredService<ARPCEDbContext>());


        services
            .AddDefaultIdentity<ARPCEUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ARPCEDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ARPCEUser, ARPCEDbContext>();


        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
        #region Repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        #endregion
        #region Services
        services.AddScoped<IStudentService, StudentService>();
        #endregion
        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
