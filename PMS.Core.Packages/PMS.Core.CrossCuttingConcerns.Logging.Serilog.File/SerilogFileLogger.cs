using Microsoft.Extensions.Configuration;
using PMS.Core.CrossCuttingConcerns.Serilog.File.Configurations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.CrossCuttingConcerns.Logging.Serilog.File;

public class SerilogFileLogger : SerilogLoggerServiceBase
{
    public SerilogFileLogger(IConfiguration configuration)
    {
        FileLogConfiguration logConfig = configuration.GetSection("SerilogLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>() ?? throw new Exception("Serilog options can't be null.");
        string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfig.FolderPath, arg1: ".txt");

        Logger = new LoggerConfiguration()
          .WriteTo.File(
          logFilePath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, fileSizeLimitBytes: 5000000,
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
          .CreateLogger();
    }
}