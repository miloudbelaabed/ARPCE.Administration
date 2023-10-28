using System.Text;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Infrastructure.Files;
using ARPCE.Administration.Infrastructure.Identity;
using ARPCE.Administration.Infrastructure.Persistence;
using ARPCE.Administration.Infrastructure.Persistence.Repositories;
using ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
using ARPCE.Administration.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
        services.AddScoped<ICourseRepository, CourseRepository>();
        #endregion
        #region Services
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ICourseService, CourseService>();
        #endregion
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
                     ).AddJwtBearer(x =>
                     {
                         x.RequireHttpsMetadata = false;
                         x.SaveToken = true;
                         x.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtConfig:Secret").Value)),
                             ValidateIssuer = false,
                             ValidateAudience = false,
                         };
                     });
/*        services.AddAuthentication()
            .AddIdentityServerJwt();*/

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
