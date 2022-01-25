using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeslaBotConsoleApp.Core.Services;
using TeslaBotConsoleApp.Models.Interfaces;

namespace TeslaBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceProvider services =
                new ServiceCollection()
                .AddSingleton<ITeslaBotService, TeslaBotService>()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddLogging(configure => configure.AddConsole())
            .BuildServiceProvider();
            var teslaBotService = services.GetService<ITeslaBotService>();
            await teslaBotService.Init();

            await Task.Delay(-1);
        }
    }
}