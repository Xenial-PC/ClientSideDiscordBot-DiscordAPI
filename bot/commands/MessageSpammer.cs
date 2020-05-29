using System.Threading;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace ClientSideSelfBot.Bot.Commands
{
    public class MessageSpammer
    {
        public static async Task SpamAsync(MessageCreateEventArgs e, string message)
        {
            FormatMessage.FormatStringAdvanced(6, message); // Format the string so we can use get only the message
            var msg = FormatMessage.AdvancedMessage; // gets the message
            var times = FormatMessage.AdvancedTimes; // gets the times it will run
            for (var i = 0; i < times; i++)
            {
                await e.Message.RespondAsync(msg).ConfigureAwait(false);
                Thread.Sleep(665); // waits .83 seconds due to discordsAPI call rate
            }
        }

        public static async Task MassDmSpamAsync(string input)
        {
            FormatMessage.FormatStringAdvanced(8, input);
            var msg = FormatMessage.AdvancedMessage;
            var times = FormatMessage.AdvancedTimes;
            var dc = Program.DiscordClient;
            foreach (var dmc in dc.PrivateChannels) // Gets all the dm channels you have
            {
                for (var i = 0; i < times; i++)
                {
                    await dmc.SendMessageAsync(msg).ConfigureAwait(false);
                    Thread.Sleep(665); // waits .83 seconds due to discordsAPI call rate
                }
            }
        }
    }
}