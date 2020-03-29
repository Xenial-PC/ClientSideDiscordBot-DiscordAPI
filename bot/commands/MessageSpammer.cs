using DSharpPlus.EventArgs;
using System.Threading.Tasks;
using WindowsInput;

namespace ClientSideSelfBot.bot.commands
{
    public class MessageSpammer
    {
        public static async Task SpamAsync(MessageCreateEventArgs e, string message)
        {
            FormatMessage.FormatStringAdvanced(6, message);
            var msg = FormatMessage._message;
            var times = FormatMessage._times;

            for (int i = 0; i < times; i++)
            {
                await e.Message.RespondAsync(msg);
                await Simulate.Events().Wait(834).Invoke();
            }
        }
    }
}
