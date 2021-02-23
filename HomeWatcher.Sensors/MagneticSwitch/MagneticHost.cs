using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWatcher.Sensors.MagneticSwitch
{
    public class MagneticHost : IHostedService
    {
        private const int PORT = 23;

        private readonly GpioController _controller;
        private readonly ILogger<MagneticHost> _logger;

        public MagneticHost(GpioController controller, ILogger<MagneticHost> logger)
        {
            _controller = controller;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started");
            _controller.OpenPin(PORT, PinMode.InputPullUp);
            _controller.RegisterCallbackForPinValueChangedEvent(PORT, PinEventTypes.Falling, Handle);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _controller.ClosePin(PORT);
            _logger.LogInformation("Stoped");
            return Task.CompletedTask;
        }

        private void Handle(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            //ToDo: send message
            _logger.LogDebug($"{DateTime.Now:HH:mm:ss} | Port[{pinValueChangedEventArgs.PinNumber}] is fall.");
        }
    }
}
