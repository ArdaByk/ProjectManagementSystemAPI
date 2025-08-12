using Microsoft.Extensions.DependencyInjection;
using PMS.Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Security.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper,JwtHelper>();
        services.AddScoped<TokenOptions>();

        return services;
    }
}
