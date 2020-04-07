using HomeWatcher.Sensors;
using HomeWatcher.TelegramCLI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeWatcher
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.RegisterTelegram();
            services.RegisterSensors();
        }

        public static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole(clo =>
            {
#if !DEBUG
                clo.DisableColors = true;
#endif
            });
#if DEBUG
            loggingBuilder.SetMinimumLevel(LogLevel.Debug);
#else
            loggingBuilder.SetMinimumLevel(LogLevel.Information);
#endif
        }
    }
}
