using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;

namespace ClientSideSelfBot
{
    public class Commands
    {
        readonly string helpCommand = @"```" +
            $"Prefix \"{Program.prefix}\"\n\n" +
            
            "User Commands: \n" +
            "• test - test the bot with test \n" +
            "• info - prints where to get your own bot\n" +
            "• msg (message)- send a message as the bot \n\n" +

            "Trusted Rank Commands: \n" +
            "• ping - pings the hosts pc with a sound \n" +
            "• game (GuessNumber) - have a chance to put my pc to sleep\n\n" +

            "Admin Rank Commands: \n" +
            "• count min, max - starts from the min and counts to the max make sure to use a comma and pls use in #counting" +
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
