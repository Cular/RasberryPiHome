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

        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>(sp => new HttpClient());
            services.AddSingleton<ITelegramBotClient, TelegramBotClient>(sp =>
            {
                var accessToken = Environment.GetEnvironmentVariable(TELEGRAMTOKEN) ?? throw new ArgumentNullException(nameof(TELEGRAMTOKEN));

                return new TelegramBotClient(accessToken, sp.GetService<HttpClient>());
            });

            return services;
        }
    }
}
