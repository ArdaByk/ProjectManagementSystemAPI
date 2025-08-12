using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PMS.Application.Abstractions.Services;
using PMS.Application.Common.Authorization;
using PMS.Application.Common.TemplateServices;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Core.Application.Pipelines.Caching;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Core.Application.Pipelines.Transaction;
using PMS.Core.Application.Pipelines.Validation;
using PMS.Core.Application.Rules;
using PMS.Core.MailKit;
using PMS.Core.Mailing;
using PMS.Core.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
       
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRule));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IProjectRoleProvider, ProjectRoleProvider>();
        services.AddScoped<IMailService, MailKitMailService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();

        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
     this IServiceCollection services,
     Assembly assembly,
     Type type,
     Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
 )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);

            else
                addWithLifeCycle(services, type);
        return services;
    }
}
