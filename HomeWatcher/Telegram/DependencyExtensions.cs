using System;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace HomeWatcher.Telegram
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

            services.AddSingleton<IMessageSender, TelegramMessageSender>();
            services.AddHostedService<TelegramMessageSender>(sp => sp.GetRequiredService<IMessageSender>() as TelegramMessageSender);

            return services;
        }
    }
}
