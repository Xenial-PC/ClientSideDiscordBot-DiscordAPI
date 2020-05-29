using DSharpPlus.EventArgs;
using System.Threading;
using System.Threading.Tasks;
// ReSharper disable TooManyChainedReferences

namespace ClientSideSelfBot.Bot.Commands
{
    public class MassCommands
    {
        public static async Task MassDmMessageDeleteAsync()
        {
            var dc = Program.DiscordClient;
            foreach (var dmc in dc.PrivateChannels) // Gets all the dm channels you have
            {
                var messages = await dmc.GetMessagesAsync().ConfigureAwait(false); // gets all the messages
                foreach (var dm in messages) // goes through the list of messages
                {
                    if (dm.Author.Id != Program.DiscordClient.CurrentUser.Id) continue;
                    await dm.Channel.DeleteMessageAsync(dm).ConfigureAwait(false);
                    Thread.Sleep(665); // Waits . secs due to discords API
                }
            }
        }

        public static async Task MassMessageDeleteAsync(MessageCreateEventArgs e)
        {
            var messages = await e.Channel.GetMessagesAsync().ConfigureAwait(false);
            foreach (var msg in messages)
            {
                await msg.Channel.DeleteMessageAsync(msg).ConfigureAwait(false);
                Thread.Sleep(665); // Waits .84 secs due to discords API
            }
        }
    }
}
