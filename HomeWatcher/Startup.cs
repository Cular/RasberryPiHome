using HomeWatcher.TelegramCLI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWatcher
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<TelegramHost>();
            Configuration.Configure(services);
        }

        public static void ConfigurationApplication(HostBuilderContext context, IConfigurationBuilder configurationBuilder)
        {
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
