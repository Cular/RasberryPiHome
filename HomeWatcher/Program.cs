using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace HomeWatcher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureLogging(Startup.ConfigureLogging)
                .ConfigureServices(Startup.ConfigureServices);
            
            await builder.RunConsoleAsync();
        }
    }
}
