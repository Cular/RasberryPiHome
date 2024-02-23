using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeWatcher.Telegram;

namespace HomeWatcher.Sensors.PIR
{
    public sealed class PirHost : IHostedService
    {
        private const int PORT = 18;

        private readonly GpioController _controller;
        private readonly IMessageSender _messageSender;
        private readonly ILogger<PirHost> _logger;

        private DateTime? _lastSendMessage;

        public PirHost(GpioController controller, IMessageSender messageSender, ILogger<PirHost> logger)
        {
            _controller = controller;
            _messageSender = messageSender;
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
            _controller.UnregisterCallbackForPinValueChangedEvent(PORT, Handle);
            _controller.ClosePin(PORT);
            _logger.LogInformation("Stopped");
            return Task.CompletedTask;
        }

        private void Handle(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            _logger.LogInformation(
                $"{DateTime.Now:HH:mm:ss} | Port[{pinValueChangedEventArgs.PinNumber}] is {pinValueChangedEventArgs.ChangeType}.");
            
            if (!IsReadyToSend())
            {
                return;
            }

            Task.Run(() => _messageSender.SendAsync());
            _lastSendMessage = DateTime.Now;
        }

        private bool IsReadyToSend() =>
            _lastSendMessage == null || DateTime.Now - _lastSendMessage.Value > TimeSpan.FromMinutes(4);
    }
}