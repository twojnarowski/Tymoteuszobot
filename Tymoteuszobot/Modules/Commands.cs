namespace Tymoteuszobot.Modules;

using Discord.Commands;
using Tymoteuszobot.Services;

public class Command : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task Ping()
    {
        await ReplyAsync("Pong");
    }

    [Command("suchar")]
    public async Task Joke()
    {
        var joke = await JokeService.GetJoke();
        await ReplyAsync(joke);
    }
}