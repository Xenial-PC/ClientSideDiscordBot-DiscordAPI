using System;
using WindowsInput;
using System.Windows.Forms;
using ClientSideSelfBot.bot.commands;
using DSharpPlus.Interactivity;
using DSharpPlus.Entities;
using System.Collections.Generic;

namespace ClientSideSelfBot
{
    public class CustomCommands
    {
        public static void Custom()
        {
            var rand = new Random();
            var correctGuess = rand.Next(1, 11);
            var roast = rand.Next(1, Roast._roasts.Count);
            var prefix = Program.prefix; // we need this incase someone changes the prefix in the config

            Program.discordClient.MessageCreated += async e =>
            {
                var discordID = e.Message.Author.Id; // gets discordID
                var ID = discordID.ToString(); // parses discordID to string
                var message = e.Message.Content; // Custom commands use the stuff inside this comment

                if (Program.selfbot || !Program.bot.Contains(ID) && !Program.isBanned(ID))
                {
                    if (e.Message.Content.Contains("source code") || e.Message.Content.Contains("find fn cheats")) // Antipaster code for fun
                    {
                        await e.Message.RespondAsync("You Dirty Twat Dont C&P!");
                    }

                    // User Commands 
                    if (e.Message.Content.StartsWith(Command("msg"))) // if the message has the command anywhere in the code it will run
                    {
                        FormatMessage.FormatString(5, message); // Formats the string to get just the message
                        var msg = FormatMessage._output; // sets the strings output to the message

                        await e.Message.RespondAsync(msg);
                    }

                    if (e.Message.Content.StartsWith(Command("info")))
                    {
                        await e.Message.RespondAsync(@"Find My Source At: " + "\n" + "https://github.com/Xenial-PC/ClientSideSelfBot");
                    }

                    if (e.Message.Content.StartsWith(Command("roast")))
                    {
                        FormatMessage.FormatString(7, message); // Formats the string to get just the name
                        var msg = FormatMessage._output; // sets the strings output to the message

                        await e.Message.RespondAsync($"{msg} {Roast._roasts[roast]}"); // responds with the name and the roast
                        roast = rand.Next(1, Roast._roasts.Count); // sets the number to a new roast
                    }

                    // Temp Commands
                    if (Program.owner.Contains(ID) || Program.selfbot || Program.admin.Contains(ID) || Program.mod.Contains(ID) || Program.trusted.Contains(ID) || Program.temp.Contains(ID))
                    {

                    }

                    // Trusted Commands
                    if (Program.owner.Contains(ID) || Program.selfbot || Program.admin.Contains(ID) || Program.mod.Contains(ID) || Program.trusted.Contains(ID))
                    {
                        if (e.Message.Content.StartsWith(Command("ping")))
                        {
                            await e.Message.RespondAsync("Pinged!");
                            Console.Beep(); // Makes a beep sound through the console on the hosts pc
                        }

                        if (e.Message.Content.StartsWith(Command("ans")))
                        {
                            Console.WriteLine(correctGuess);
                        }

                        if (e.Message.Content.StartsWith(Command("game")))
                        {
                            if (e.Message.Content.Contains(Command("game " + correctGuess)))
                            {
                                await e.Message.RespondAsync("Correct!");
                                correctGuess = rand.Next(1, 11);
                                Sleep();
                            }
                            else
                            {
                                await e.Message.RespondAsync("Wrong! Game Ended");
                                correctGuess = rand.Next(1, 11);
                            }
                        }
                    }

                    // Mod Commands
                    if (Program.owner.Contains(ID) || Program.selfbot || Program.admin.Contains(ID) || Program.mod.Contains(ID))
                    {

                    }

                    // Admin Commands
                    if (Program.owner.Contains(ID) || Program.selfbot || Program.admin.Contains(ID))
                    {
                        if (e.Message.Content.StartsWith(Command("count")))
                        {
                            FormatMessage.FormatStringInt(7, message);
                            var minNum = FormatMessage._min; // gets the min output from formatting the message
                            var maxNum = FormatMessage._max; // gets the max output from formatting the message

                            for (int x = minNum; x < maxNum + 1; x++)
                            {
                                await e.Message.RespondAsync(x.ToString());
                                await Simulate.Events().Wait(834).Invoke(); // .83 seconds wait time due to discords api call rate
                            }
                        }
                    }

                    // Owner Commands
                    if (Program.owner.Contains(ID) || Program.selfbot)
                    {
                        if (e.Message.Content.StartsWith(Command("spam")))
                        {
                            await MessageSpammer.SpamAsync(e, message);
                        }

                        if (e.Message.Content.StartsWith(Command("mass.delete.dm")))
                        {
                            await MassCommands.MassDMMessageDeleteAsync();
                        }

                        if (e.Message.Content.StartsWith(Command("mass.dm")))
                        {
                            await MessageSpammer.MassDMSpamAsync(message);
                        }

                        if (e.Message.Content.StartsWith(Command("mass.delete.msg")))
                        {
                            await MassCommands.MassMessageDeleteAsync(e);
                        }
                    }
                }
            };
        }

        public static void Sleep()
        {
            Application.SetSuspendState(PowerState.Suspend, true, false);
        }

        public static string Command(string command)
        {
            var prefix = Program.prefix;
            return $"{prefix}{command}";
        }
    }
}
