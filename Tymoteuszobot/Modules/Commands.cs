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

    [Command("remindme", RunMode = RunMode.Async)]
    public async Task Reminder([Discord.Interactions.Summary(description: "Podaj co ile godzin ma być przypominanie")] int hours = 24)
    {
        RemindersService.AddReminder(Context.User.Mention, hours);
        await ReplyAsync($"Będę przypominał o nauce co {hours} godzin!");

        while (RemindersService.Exists(Context.User.Mention))
        {
            int waitTime = RemindersService.GetReminder(Context.User.Mention).HoursBetween;
            await Task.Run(async () =>
            {
                await Task.Delay(waitTime /** 60  * 60 */* 1000)
                          .ContinueWith(t =>
                              {
                                  if (RemindersService.Exists(Context.User.Mention))
                                  {
                                      ReplyAsync(Context.User.Mention + " do nauki leniu!");
                                      Console.WriteLine($"Reminder sent at {DateTime.Now.ToString()} to {Context.User.Mention}");
                                  }
                              });
            });
        }
    }

    [Command("unremindme")]
    public async Task Reminder()
    {
        RemindersService.DeleteReminder(Context.User.Mention);
        await ReplyAsync($"Koniec przypominania!");
        Console.WriteLine($"Unreminder sent at {DateTime.Now.ToString()} to {Context.User.Mention}");
    }
}