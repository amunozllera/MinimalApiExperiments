using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace LearningMinimalAPi.Startup
{
    public static class Logging
    {
        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            var logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            builder.Logging.AddSerilog(logger);
            builder.Host.UseSerilog(logger);
        }

        public static void AddLoggingSingleton(this IServiceCollection services)
        {
            //services.AddSingleton<ILogger, Logger>();
        }
    }
}
