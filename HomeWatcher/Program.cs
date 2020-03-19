using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HomeWatcher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureLogging(Startup.ConfigureLogging)
                .ConfigureServices(Startup.ConfigureServices)
                .ConfigureAppConfiguration(Startup.ConfigurationApplication);

            await builder.RunConsoleAsync();
        }
    }
}
