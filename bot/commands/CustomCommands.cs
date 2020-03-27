using System;
using WindowsInput;
using System.Windows.Forms;

namespace ClientSideSelfBot
{
    public class CustomCommands
    {
        public static void Custom()
        {
            var rand = new Random();
            var correctGuess = rand.Next(1, 11);

            Program.discordClient.MessageCreated += async e =>
            {
                var discordID = e.Message.Author.Id; // gets discordID
                var ID = discordID.ToString(); // parses discordID to string
                var commands = e.Message.Content; // Custom commands use the stuff inside this comment

                if (!Program.bot.Contains(ID) && !Program.selfbot)
                {
                    if (e.Message.Content.Contains("source code") || e.Message.Content.Contains("find fn cheats")) // Antipaster code for fun
                    {
                        await e.Message.RespondAsync("You Dirty Twat Dont C&P!");
                    }

                    // User Commands 
                    if (e.Message.Content.Contains(";msg")) // if the message has the command anywhere in the code it will run
                    {
                        var msg = commands.Substring(commands.IndexOf((";"))); // Starts a new string at the index of ; << and makes that the new starting point /0
                        msg = msg.Remove(0, 5); // Removes everything up to the words

                        await e.Message.RespondAsync(msg);
                    }

                    // Temp Commands
                    if (Program.owner.Contains(ID) || Program.admin.Contains(ID) || Program.mod.Contains(ID) || Program.trusted.Contains(ID) || Program.temp.Contains(ID))
                    {

                    }

                    // Trusted Commands
                    if (Program.owner.Contains(ID) || Program.admin.Contains(ID) || Program.mod.Contains(ID) || Program.trusted.Contains(ID))
                    {
                        if (e.Message.Content.Contains(";ping"))
                        {
                            await e.Message.RespondAsync("Pinged!");
                            Console.Beep(); // Makes a beep sound through the console on the hosts pc
                        }

                        if (e.Message.Content.Contains(";ans"))
                        {
                            Console.WriteLine(correctGuess);
                        }

                        if (e.Message.Content.Contains(";game"))
                        {
                            if (e.Message.Content.Contains(";game " + correctGuess))
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
                    if (Program.owner.Contains(ID) || Program.admin.Contains(ID) || Program.mod.Contains(ID))
                    {

                    }

                    // Admin Commands
                    if (Program.owner.Contains(ID) || Program.admin.Contains(ID))
                    {
                        if (e.Message.Content.Contains(";count"))
                        {
                            var msg = commands.Substring(commands.IndexOf((";"))); // Starts a new string at the index of ; << and makes that the new starting point /0
                            msg = msg.Remove(0, 7); // removes the ;count + the space

                            // Starts at the other side of the number and removes the rest of the string
                            var minNum = int.Parse(msg.Remove(msg.IndexOf(","), commands.Substring(commands.IndexOf(",")).Length));

                            msg = commands.Substring(commands.IndexOf(",")); // Starts a new string at the index of , << /0
                            var maxNum = int.Parse(msg.Remove(0, 2)); // Removes the space and only uses the numbers in the string

                            for (int x = minNum; x < maxNum + 1; x++)
                            {
                                await e.Message.RespondAsync(x.ToString());
                                await Simulate.Events().Wait(900).Invoke(); // .9 seconds wait time due to discords api call rate
                            }
                        }
                    }

                    // Owner Commands
                    if (Program.owner.Contains(ID))
                    {

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
