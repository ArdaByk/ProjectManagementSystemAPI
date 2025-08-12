using Microsoft.Extensions.DependencyInjection;
using PMS.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

}
