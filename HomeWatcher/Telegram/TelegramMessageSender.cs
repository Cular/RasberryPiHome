using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace HomeWatcher.Telegram;

internal class TelegramMessageSender : IMessageSender, IHostedService
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly ILogger<TelegramMessageSender> _logger;
    private readonly List<long> _chatIds = new(2);

    public TelegramMessageSender(ITelegramBotClient telegramBotClient, ILogger<TelegramMessageSender> logger)
    {
        _telegramBotClient = telegramBotClient;
        _logger = logger;
    }

    public async Task SendAsync()
    {
        if (!_chatIds.Any())
        {
            _logger.LogError("No chat ids! Messages will not send");
            return;
        }

        foreach (var chatId in _chatIds)
        {
            try
            {
                var res = await _telegramBotClient.SendTextMessageAsync(chatId, "Cat detected");
                _logger.LogDebug($"Received {res.Chat?.Username} a message");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed message sending");
            }
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var result = await _telegramBotClient.GetUpdatesAsync(cancellationToken: cancellationToken);
        foreach (var update in result)
        {
            var chatId = update?.Message?.Chat?.Id;
            if (chatId != null)
                _chatIds.Add(chatId.Value);
        }

        if (!_chatIds.Any())
            _logger.LogError("No chat ids! Messages will not send");
        else
            _logger.LogDebug($"Received {_chatIds.Count} chats");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}