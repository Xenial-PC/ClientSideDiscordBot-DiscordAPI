using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ClientSideSelfBot.bot.logs
{
    public class AdvancedLogger
    {
        private static string path = $"logs/LogOf_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt"; // Path of the log file

        public static void LoadAdvancedLoggerConfig()
        {
            try
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(nameof(logs)); // Creates a new Directory for the log files
                    File.WriteAllText(path, ""); // Makes a new log file
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public static Task MessageLoggerAsync()
        {
            if (File.Exists(path))
            {
                Program.discordClient.MessageCreated += async e =>
                {
                    if (e.Channel.Type == ChannelType.Private)
                    {
                        var message = MessageLog(e, "DM"); // If channel is private then make the message type DM
                        File.AppendAllText(path, message); // writes the message to the file
                    }
                    else if (e.Channel.Type == ChannelType.Text)
                    {
                        var message = MessageLog(e, $"{e.Guild.Name}"); // if channel is Text(Normal Channel) print the channel name
                        File.AppendAllText(path, message); // writes the message to the file
                    }
                    else
                    {
                        var message = MessageLog(e, $"{e.Channel.Type}"); // if channel is not private or text just print the channel type
                        File.AppendAllText(path, message); // writes the message to the file
                    }
                };
            }
            return Task.CompletedTask;
        }

        public static string MessageLog(MessageCreateEventArgs e,  string channelType)
        {
            var message = $"[{DateTime.Now.ToLongDateString()} : {DateTime.Now.ToShortTimeString()}] {e.Author.Username}#{e.Author.Discriminator} " +
                        $"| Message: \"{e.Message.Content}\" | {channelType} \n"; // Template for the message
            return message;
        }
    }
}
