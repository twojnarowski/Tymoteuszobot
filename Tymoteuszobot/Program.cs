using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Tymoteuszobot;

public class Program
{
    private DiscordSocketClient? _client;
    private LoggingService? _loggingService;
    private CommandService? _commands;
    private IServiceProvider? _services;

    public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

    public async Task MainAsync()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        };

        _client = new DiscordSocketClient(config);
        _commands = new CommandService();
        _loggingService = new LoggingService(_client, _commands);
        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .BuildServiceProvider();

        var token = File.ReadAllText("token.txt");

        _client.Log += Log;

        await RegisterCommandsAsync();

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public async Task RegisterCommandsAsync()
    {
        if (_client is not null)
        {
            _client.MessageReceived += HandleCommandAsync;
        }
        if (_commands is not null)
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
    }

    private async Task HandleCommandAsync(SocketMessage arg)
    {
        var message = arg as SocketUserMessage;
        var context = new SocketCommandContext(_client, message);

        if (message is null) return;

        if (message.Author.IsBot)
        {
            return;
        }

        int argPos = 0;

        if (message.HasStringPrefix("!", ref argPos) && _commands is not null)
        {
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.ErrorReason);
            }
            if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}