using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Telegram.Bot;

namespace HomeWatcher.TelegramCLI
{
    public static class Configuration
    {
        private const string TELEGRAMTOKEN = "TELEGRAM__TOKEN";

        public static IServiceCollection RegisterTelegram(this IServiceCollection services)
        {
            services.AddSingleton<ITelegramBotClient, TelegramBotClient>(sp =>
            {
                var accessToken = Environment.GetEnvironmentVariable(TELEGRAMTOKEN) ?? throw new ArgumentNullException(nameof(TELEGRAMTOKEN));
                return new TelegramBotClient(accessToken);
            });

            return services;
        }
    }
}
