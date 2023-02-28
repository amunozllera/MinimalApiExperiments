using Serilog;

namespace MinimalApi.Startup
{
    public static class Logging
    {
        public static Serilog.Core.Logger AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            var logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            builder.Logging.AddSerilog(logger);
            builder.Host.UseSerilog(logger);
            return logger;
        }
    }
}
