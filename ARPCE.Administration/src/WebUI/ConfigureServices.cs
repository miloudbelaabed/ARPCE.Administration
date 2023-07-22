using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Infrastructure.Persistence;
using ARPCE.Administration.WebUI.Filters;
using ARPCE.Administration.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;


namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ARPCEDbContext>();

    /*    services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        services.AddRazorPages();*/

        // Customise default API behaviour
     /*   services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);*/

       

        return services;
    }
}
