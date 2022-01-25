using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeslaBotConsoleApp.Models.Interfaces;

namespace TeslaBotConsoleApp.Core.Services
{
    public class TeslaBotService : ITeslaBotService
    {
        private readonly ILogger<TeslaBotService> _logger;
        private readonly DiscordSocketClient _discordBotClient;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _serviceProvider;

        public TeslaBotService(ILoggerFactory logger, DiscordSocketClient discordSocketClient, CommandService commandService, IServiceProvider serviceProvider)
        {
            _logger = logger.CreateLogger<TeslaBotService>();
            _discordBotClient = discordSocketClient;
            _commandService = commandService;
            _serviceProvider = serviceProvider;
            _discordBotClient.Log += Log;
        }

        public async Task Init()
        {
             await _discordBotClient.LoginAsync(TokenType.Bot, "X");
             await _discordBotClient.StartAsync();
             _discordBotClient.MessageReceived += HandleCommandAsync;
             await _commandService.AddModuleAsync<TeslaBotCommandService>(_serviceProvider);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null) return;
            var context = new SocketCommandContext(_discordBotClient, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos))
            {
                var result = await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
                if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        private Task Log(LogMessage logMessage)
        {
            _logger.Log( LogLevel.Information, logMessage.Message);
            return Task.CompletedTask;
        }
    }
}
