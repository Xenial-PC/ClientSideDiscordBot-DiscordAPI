using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;
using WindowsInput;

namespace ClientSideSelfBot.bot.commands
{
    public class MessageSpammer
    {
        public static async Task SpamAsync(MessageCreateEventArgs e, string message)
        {
            FormatMessage.FormatStringAdvanced(6, message); // Format the string so we can use get only the message
            var msg = FormatMessage._message; // gets the message
            var times = FormatMessage._times; // gets the times it will run

            for (int i = 0; i < times; i++)
            {
                await e.Message.RespondAsync(msg).ConfigureAwait(false);
                await Simulate.Events().Wait(834).Invoke().ConfigureAwait(false); // waits .83 seconds due to discordsAPI call rate
            }
        }

        public static async Task MassDMSpamAsync(string input)
        {
            FormatMessage.FormatStringAdvanced(8, input);

            var msg = FormatMessage._message;
            var times = FormatMessage._times;

            var DC = Program.discordClient;
            foreach (DiscordDmChannel DMC in DC.PrivateChannels) // Gets all the dm channels you have
            {
                for (int i = 0; i < times; i++)
                {
                    await DMC.SendMessageAsync(msg);
                    await Simulate.Events().Wait(934).Invoke(); // waits .83 seconds due to discordsAPI call rate
                }
            }
        }
    }
}
