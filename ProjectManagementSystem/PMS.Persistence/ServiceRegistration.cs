using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Application.Abstractions.Services;
using PMS.Persistence.Contexts;
using PMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("PMS")));

        services.AddScoped<IEmailVerificationTokenService, EmailVerificationManager>();
        services.AddScoped<IProjectInvitationService, ProjectInvitationManager>();
        services.AddScoped<IProjectService, ProjectManager>();
        services.AddScoped<IProjectTaskService, ProjectTaskManager>();
        services.AddScoped<IProjectUserService, ProjectUserManager>();
        services.AddScoped<IProjectTaskUserService, ProjectTaskUserManager>();
        services.AddScoped<IUserService, UserManager>();

        return services;
    }
}
