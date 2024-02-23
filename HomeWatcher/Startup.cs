using HomeWatcher.Sensors;
using HomeWatcher.Telegram;
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
            loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(LogLevel.Information);
        }
    }
}
