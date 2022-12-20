using Discord;
using Discord.Interactions;
using Tymoteuszobot.Services;

namespace Tymoteuszobot.Modules;

internal class Interactions : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; }
    public CommandHandler _handler;

    public Interactions(CommandHandler handler)
    {
        _handler = handler;
    }

    [SlashCommand("ping", "Pings the bot and returns its latency.")]
    public async Task Ping()
        => await RespondAsync(text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!", ephemeral: true);

    [UserCommand("greet")]
    public async Task GreetUserAsync(IUser user)
        => await RespondAsync(text: $":wave: {Context.User} said hi to you, <@{user.Id}>!");
}