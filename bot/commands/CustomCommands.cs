using System;
using WindowsInput;
using System.Windows.Forms;
using ClientSideSelfBot.bot.commands;

namespace ClientSideSelfBot
{
    public class CustomCommands
    {
        public static void Custom()
        {
            var rand = new Random();
            var correctGuess = rand.Next(1, 11);
            var comeback = rand.Next(1, 5);
            var prefix = Program.prefix; // we need this incase someone changes the prefix in the config

            Program.discordClient.MessageCreated += async e =>
            {
                var discordID = e.Message.Author.Id; // gets discordID
                var ID = discordID.ToString(); // parses discordID to string
                var commands = e.Message.Content; // Custom commands use the stuff inside this comment

                if (Program.selfbot || !Program.bot.Contains(ID))
                {
                    if (e.Message.Content.Contains("source code") || e.Message.Content.Contains("find fn cheats")) // Antipaster code for fun
                    {
                        await e.Message.RespondAsync("You Dirty Twat Dont C&P!");
                    }

                    // User Commands 
                    if (e.Message.Content.Contains($"{prefix}msg")) // if the message has the command anywhere in the code it will run
                    {
                        FormatMessage.FormatString(5, e.Message.Content.ToString());
                        var msg = FormatMessage._output;

                        await e.Message.RespondAsync(msg);
                    }

                    if (e.Message.Content.Contains($"{prefix}info"))
                    {
                        await e.Message.RespondAsync(@"Find My Source At: " + "\n" + "https://github.com/Xenial-PC/ClientSideSelfBot");
                    }

                    if (e.Message.Content.Contains($"{prefix}roast"))
                    {
                        FormatMessage.FormatString(7, commands);
                        var message = FormatMessage._output;

                        switch (comeback)
                        {
                            case 1:
                                await e.Message.RespondAsync(message + " What a loser!");
                                comeback = rand.Next(1, 6);
                                break;
                            case 2:
                                await e.Message.RespondAsync(message + " Paster Much?!");
                                comeback = rand.Next(1, 6);
                                break;
                            case 3:
                                await e.Message.RespondAsync(message + " What a sad little skid!");
                                comeback = rand.Next(1, 6);
                                break;
                            case 4:
                                await e.Message.RespondAsync(message + " Want free stuff go ask your parents you skid!");
                                comeback = rand.Next(1, 6);
                                break;
                            case 5:
                                await e.Message.RespondAsync(message + " no ones listening to you, you poor little baby? gunna cry LMAO");
                                comeback = rand.Next(1, 6);
                                break;
                            default:
                                await e.Message.RespondAsync(message + " ah fuck off!");
                                break;
                        }
                    }

                    // Temp Commands
                    if (Program.owner.Contains(ID) || Program.selfbot || Program.admin.Contains(ID) || Program.mod.Contains(ID) || Program.trusted.Contains(ID) || Program.temp.Contains(ID))
                    {

                    }

                    // Trusted Commands
                    if (Program.owner.Contains(ID) || Program.selfbot || Program.admin.Contains(ID) || Program.mod.Contains(ID) || Program.trusted.Contains(ID))
                    {
                        if (e.Message.Content.Contains($"{prefix}ping"))
                        {
                            await e.Message.RespondAsync("Pinged!");
                            Console.Beep(); // Makes a beep sound through the console on the hosts pc
                        }

                        if (e.Message.Content.Contains($"{prefix}ans"))
                        {
                            Console.WriteLine(correctGuess);
                        }

                        if (e.Message.Content.Contains($"{prefix}game"))
                        {
                            if (e.Message.Content.Contains($"{prefix}game " + correctGuess))
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
                        if (e.Message.Content.Contains($"{prefix}count"))
                        {
                            FormatMessage.FormatStringInt(7, e.Message.Content.ToString());
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
                        var msg = e.Message.Content.ToString();
                        if (e.Message.Content.Contains($"{prefix}spam"))
                        {
                            await MessageSpammer.SpamAsync(e, msg);
                        }
                    }
                }
            };
        }

        public static void Sleep()
        {
            Application.SetSuspendState(PowerState.Suspend, true, false);
        }
    }
}
