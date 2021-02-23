using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWatcher.Sensors.PIR
{
    public class PirHost : IHostedService
    {
        private const int PORT = 18;

        private readonly GpioController _controller;
        private readonly ILogger<PirHost> _logger;

        public PirHost(GpioController controller, ILogger<PirHost> logger)
        {
            _controller = controller;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started");
            _controller.OpenPin(PORT, PinMode.Input);
            _controller.RegisterCallbackForPinValueChangedEvent(PORT, PinEventTypes.Rising, Handle);
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
            _logger.LogDebug($"{DateTime.Now:HH:mm:ss} | Port[{pinValueChangedEventArgs.PinNumber}] is rising.");
        }
    }
}
