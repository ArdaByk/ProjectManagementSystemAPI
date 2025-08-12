using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Core.CrossCuttingConcerns.Logging.Abstraction;
using PMS.Core.CrossCuttingConcerns.Logging.Serilog;
using PMS.Core.CrossCuttingConcerns.Logging.Serilog.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.CrossCuttingConcerns.Logging.DependencyInjection;

public static class ServiceCollectionLoggingExtensions
{
    public static IServiceCollection AddLoggingServices(this IServiceCollection services)
    {
        services.AddScoped<SerilogLoggerServiceBase,SerilogFileLogger >();
        services.AddScoped<ILogger, SerilogFileLogger>();

        return services;
    }
}
