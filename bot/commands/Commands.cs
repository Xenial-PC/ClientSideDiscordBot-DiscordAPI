using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;

namespace ClientSideSelfBot
{
    public class Commands
    {
        readonly string helpCommand = @"```" +
            "Commands: \n" +
            "Prefix ;\n\n" +

            "User Commands: \n" +
            "• test - test the bot with test \n" +
            "• msg (message)- send a message as the bot \n" +
            "• count min, max - starts from the min and counts to the max use in #counting\n\n" +

            "Trusted Rank Commands: \n" +
            "• ping - pings the hosts pc with a sound \n" +
            "```"; // Our customized help command

        [Command("test"), Description("Prints Test")] // commands go up here
        public async Task TestAsync(CommandContext c) // this could be named anything but name it after the command
        {
            await c.RespondAsync("Test!");
        }

        [Command("cmds"), Description("Prints out all commands")]
        public async Task CmdsAsync(CommandContext c)
        {
            await c.RespondAsync(helpCommand);
        }

        [Command("help"), Description("Prints out all commands")]
        public async Task HelpAsync(CommandContext c)
        {
            await c.RespondAsync(helpCommand);
        }

        [Command("date"), Description("Prints the current date")]
        public async Task DateAsync(CommandContext c)
        {
            await c.RespondAsync(DateTime.Now.ToLongDateString()); // returns the date time as a long string
        }
    }
}   
