using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsInput;

namespace ClientSideSelfBot.bot.commands
{
    public class MassCommands
    {
        public async static Task MassDMMessageDeleteAsync()
        {
            var DC = Program.discordClient;
            foreach (DiscordDmChannel DMC in DC.PrivateChannels) // Gets all the dm channels you have
            {
                var messages = await DMC.GetMessagesAsync(); // gets all the messages
                foreach (DiscordMessage DM in messages) // goes through the list of messages
                {
                    if (DM.Author.Id == Program.discordClient.CurrentUser.Id) // checks for the bots ID
                    {
                        await DM.Channel.DeleteMessageAsync(DM);
                        await Simulate.Events().Wait(843).Invoke(); // Waits .84 secs due to discords API
                    }
                }
            }
        }

        public async static Task MassMessageDeleteAsync(MessageCreateEventArgs e)
        {
            var messages = await e.Channel.GetMessagesAsync();

            foreach (DiscordMessage msg in messages)
            {
                await msg.Channel.DeleteMessageAsync(msg);
                await Simulate.Events().Wait(843).Invoke(); // Waits .84 secs due to discords API
            }
        }
    }
}
