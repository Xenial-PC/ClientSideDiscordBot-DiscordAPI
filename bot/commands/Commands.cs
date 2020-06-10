using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ClientSideSelfBot.Bot.Roles;
// ReSharper disable ExceptionNotDocumented
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

// ReSharper disable ComplexConditionExpression

namespace ClientSideSelfBot.Bot.Commands
{
    public class Commands
    {
        static readonly string HelpCommand = @"```" +
                                       $"SelfBot: {Program.Selfbot} \n" +
                                       $"Prefix \"{Program.Prefix}\"\n\n" +

                                       "User Commands: \n" +
                                       "• test - test the bot with test \n" +
                                       "• info - prints where to get your own bot\n" +
                                       "• msg (message)- send a message as the bot \n" +
                                       "• roast (person) - ping a person and it will roast them\n\n" +

                                       "Trusted Rank Commands: \n" +
                                       "• ping - pings the hosts pc with a sound \n" +
                                       "• game (GuessNumber) - have a chance to put my pc to sleep\n\n" +

                                       "Admin Rank Commands: \n" +
                                       "• count min, max - starts from the min and counts to the max make sure to use a comma and pls use in #counting\n\n" +

                                       "Owner Rank Commands: \n" +
                                       "• spam message, times - message and times have to be separated by a comma\n" +
                                       "• \n\n" +

                                       "Mass Commands: \n" +
                                       "• mass.delete.msg - deletes every message in the channel\n" +
                                       "• mass.dm message, times - message and times have to be separated by a comma: dms everyone that has sent you a dm in the past\n" +
                                       "• mass.delete.dm - deletes everything you've said in the dms\n" +
                                       "```"; // Our customized help command
        public static void Custom()
        {
            var rand = new Random();
            var correctGuess = rand.Next(1, 11);
            var roast = rand.Next(1, Roast.Roasts.Count);
            Program.DiscordClient.MessageCreated += async e =>
            {
                var discordId = e.Message.Author.Id; // gets discordIDs
                var id = discordId.ToString(); // parses discordID to string
                var message = e.Message.Content; // Custom commands use the stuff inside this comment
                if (RoleProperties.BannedList.Contains(id)) return;
                if (Program.Selfbot || !Program.Bot.Contains(id))
                {
                    // Fun commands
                    if (e.Message.Content.Contains("source code")) // Anti-Paster code for fun
                    {
                        await e.Message.RespondAsync("You Dirty Twat Dont C&P!").ConfigureAwait(false);
                    }

                    if (e.Message.Content.Contains("tranny")) // Anti-Paster code for fun
                    {
                        await e.Message.RespondAsync("Dont say that word please!").ConfigureAwait(false);
                    }

                    // User Commands 
                    if (e.Message.Content.StartsWith(Command("test")))
                    {
                        await e.Message.RespondAsync("Test").ConfigureAwait(false);
                    }

                    if (e.Message.Content.StartsWith(Command("date")))
                    {
                        await e.Message.RespondAsync(DateTime.Now.ToLongDateString()).ConfigureAwait(false);
                    }

                    if (e.Message.Content.StartsWith(Command("help")) || e.Message.Content.Contains(Command("cmds")))
                    {
                        await e.Message.RespondAsync(HelpCommand).ConfigureAwait(false);
                    }

                    if (e.Message.Content.StartsWith(Command("msg"))) // if the message has the command anywhere in the code it will run
                    {
                        FormatMessage.FormatString(5, message); // Formats the string to get just the message
                        var msg = FormatMessage.Output; // sets the strings output to the message
                        await e.Message.RespondAsync(msg).ConfigureAwait(false);
                    }

                    if (e.Message.Content.StartsWith(Command("info")))
                    {
                        await e.Message.RespondAsync(@"Find My Source At: " + "\n" + "https://github.com/Xenial-PC/ClientSideSelfBot").ConfigureAwait(false);
                    }

                    if (e.Message.Content.StartsWith(Command("roast")))
                    {
                        FormatMessage.FormatString(7, message); // Formats the string to get just the name
                        var msg = FormatMessage.Output; // sets the strings output to the message
                        await e.Message.RespondAsync($"{msg} {Roast.Roasts[roast]}").ConfigureAwait(false); // responds with the name and the roast
                        roast = rand.Next(1, Roast.Roasts.Count); // sets the number to a new roast
                    }

                    // Temp Commands
                    if (Program.Owner.Contains(id) || Program.Selfbot || RoleProperties.AdminList.Contains(id) || RoleProperties.ModList.Contains(id) || RoleProperties.TrustedList.Contains(id) || RoleProperties.TempList.Contains(id))
                    {

                    }

                    // Trusted Commands
                    if (Program.Owner.Contains(id) || Program.Selfbot || RoleProperties.AdminList.Contains(id) || RoleProperties.ModList.Contains(id) || RoleProperties.TrustedList.Contains(id))
                    {
                        if (e.Message.Content.StartsWith(Command("ping")))
                        {
                            await e.Message.RespondAsync("Pinged!").ConfigureAwait(false);
                            Console.Beep(); // Makes a beep sound through the console on the hosts pc
                        }
                        if (e.Message.Content.StartsWith(Command("game")))
                        {
                            if (e.Message.Content.Contains(Command("game " + correctGuess)))
                            {
                                await e.Message.RespondAsync("Correct!").ConfigureAwait(false);
                                correctGuess = rand.Next(1, 11);
                                Sleep();
                            }
                            else
                            {
                                await e.Message.RespondAsync("Wrong! Game Ended").ConfigureAwait(false);
                                correctGuess = rand.Next(1, 11);
                            }
                        }
                    }

                    // Mod Commands
                    if (Program.Owner.Contains(id) || Program.Selfbot || RoleProperties.AdminList.Contains(id) || RoleProperties.ModList.Contains(id))
                    {

                    }

                    // Admin Commands
                    if (Program.Owner.Contains(id) || Program.Selfbot || RoleProperties.AdminList.Contains(id))
                    {
                        if (e.Message.Content.StartsWith(Command("count")))
                        {
                            FormatMessage.FormatStringInt(7, message);
                            var minNum = FormatMessage.IntMin; // gets the min output from formatting the message
                            var maxNum = FormatMessage.IntMax; // gets the max output from formatting the message
                            for (var x = minNum; x < maxNum + 1; x++)
                            {
                                await e.Message.RespondAsync(x.ToString()).ConfigureAwait(false);
                                Thread.Sleep(665); // .655 seconds wait time due to discords api call rate
                            }
                        }
                        if (e.Message.Content.StartsWith(Command("add")))
                        {
                            FormatMessage.FormatStrings(8, e.Message.Content);
                            if (e.Message.Content.Contains("admin"))
                            {
                                AddUser(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                                RoleProperties.AdminList.Add(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                            }
                            else if (e.Message.Content.Contains("mod"))
                            {
                                AddUser(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                                RoleProperties.ModList.Add(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                            }
                            else if (e.Message.Content.Contains("trusted"))
                            {
                                AddUser(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                                RoleProperties.TrustedList.Add(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                            }
                            else if (e.Message.Content.Contains("temp"))
                            {
                                AddUser(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                                RoleProperties.TempList.Add(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                            }
                        }
                        if (e.Message.Content.StartsWith(Command("ban")))
                        {
                            FormatMessage.FormatString(8, e.Message.Content);
                            AddUser(FormatMessage.AdvancedOutputOne.Trim().TrimEnd('>'));
                            await e.Message.RespondAsync($"Banned! <@!{FormatMessage.Output}").ConfigureAwait(false);
                            RoleProperties.BannedList.Add(FormatMessage.Output.Trim().TrimEnd('>'));
                        }
                    }

                    // Owner Commands
                    if (Program.Owner.Contains(id) || Program.Selfbot)
                    {
                        if (e.Message.Content.StartsWith(Command("spam")))
                        {
                            await MessageSpammer.SpamAsync(e, message).ConfigureAwait(false);
                        }
                        if (e.Message.Content.StartsWith(Command("mass.delete.dm")))
                        {
                            await MassCommands.MassDmMessageDeleteAsync().ConfigureAwait(false);
                        }
                        if (e.Message.Content.StartsWith(Command("mass.dm")))
                        {
                            await MessageSpammer.MassDmSpamAsync(message).ConfigureAwait(false);
                        }
                        if (e.Message.Content.StartsWith(Command("mass.delete.msg")))
                        {
                            await MassCommands.MassMessageDeleteAsync(e).ConfigureAwait(false);
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
            var prefix = Program.Prefix;
            return $"{prefix}{command}";
        }

        public static void ChangeRank(string user, string path)
        {
            foreach (var line in File.ReadAllLines(path))
            {
                if (line.Contains(user)) line.Remove(0, user.Length);
                Console.WriteLine(user);
                Console.WriteLine(line);
            }
        }

        public static void AddUser(string user)
        {
            if (RoleProperties.AdminList.Contains(user))
            {
                ChangeRank(user, "Roles/Admin.txt");
                RoleProperties.AdminList.Remove(user);
                File.AppendAllText("Roles/Admin.txt", $"{user}" + "\n");
            }
            if (RoleProperties.ModList.Contains(user))
            {
                ChangeRank(user, "Roles/Mod.txt");
                RoleProperties.ModList.Remove(user);
                File.AppendAllText("Roles/Mod.txt", $"{user}" + "\n");
            }
            if (RoleProperties.TrustedList.Contains(user))
            {
                ChangeRank(user, "Roles/Trusted.txt");
                RoleProperties.TrustedList.Remove(user);
                File.AppendAllText("Roles/Trusted.txt", $"{user}" + "\n");
            }
            if (RoleProperties.TempList.Contains(user))
            {
                ChangeRank(user, "Roles/Temp.txt");
                RoleProperties.TempList.Remove(user);
                File.AppendAllText("Roles/Temp.txt", $"{user}" + "\n");
            }
            if (RoleProperties.BannedList.Contains(user))
            {
                ChangeRank(user, "Roles/Banned.txt");
                RoleProperties.BannedList.Remove(user);
                File.AppendAllText("Roles/Banned.txt", $"{user}" + "\n");
            }
        }
    }
}