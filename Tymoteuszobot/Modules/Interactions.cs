using Discord;
using Discord.Interactions;

namespace Tymoteuszobot.Modules
{
    internal class Interactions : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands;

        public Interactions(InteractionService commands)
        {
            Commands = commands;
        }

        [UserCommand("remindthem")]
        public async Task RemindUserAsync(IUser user)
            => await RespondAsync($"@{user.Id} do nauki leniu!");
    }
}