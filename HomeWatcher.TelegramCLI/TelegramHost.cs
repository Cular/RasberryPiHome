using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace HomeWatcher.TelegramCLI
{
    public class TelegramHost : IHostedService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<TelegramHost> _logger;
        private CancellationToken _cancellationToken;

        public TelegramHost(ITelegramBotClient botClient, ILogger<TelegramHost> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _botClient.OnMessage += _botClient_OnMessage;
            _botClient.StartReceiving();
            _cancellationToken = cancellationToken;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _botClient.StopReceiving();
            _botClient.OnMessage -= _botClient_OnMessage;
            return Task.CompletedTask;
        }

        private async void _botClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                await _botClient.SendTextMessageAsync(e.Message.Chat.Id, "I receive your message", replyToMessageId: e.Message.MessageId, cancellationToken: _cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
