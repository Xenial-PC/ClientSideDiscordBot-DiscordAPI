using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.IO;
using System.Threading.Tasks;
// ReSharper disable ComplexConditionExpression
// ReSharper disable ExceptionNotDocumented

namespace ClientSideSelfBot.Bot.Logs
{
    public class AdvancedLogger
    {
        private static readonly string Path = $"logs/LogOf_{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt"; // Path of the log file

        public static void LoadAdvancedLoggerConfig()
        {
            try
            {
                if (File.Exists(Path)) return;
                Directory.CreateDirectory(nameof(Logs)); // Creates a new Directory for the log files
                File.WriteAllText(Path, ""); // Makes a new log file
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
            if (File.Exists(Path)) Program.DiscordClient.MessageCreated += OnDiscordClientOnMessageCreatedAsync;
            return Task.CompletedTask;
        }

        private static Task OnDiscordClientOnMessageCreatedAsync(MessageCreateEventArgs e)
        {
            switch (e.Channel.Type)
            {
                case ChannelType.Private:
                {
                    var message = MessageLog(e, "DM"); // If channel is private then make the message type DM
                    File.AppendAllText(Path, message); // writes the message to the file
                }break;
                case ChannelType.Text:
                {
                    var message = MessageLog(e, $"{e.Guild.Name}"); // if channel is Text(Normal Channel) print the channel name
                    File.AppendAllText(Path, message); // writes the message to the file
                }break;
                default:
                {
                    var message = MessageLog(e, $"{e.Channel.Type}"); // if channel is not private or text just print the channel type
                    File.AppendAllText(Path, message); // writes the message to the file
                }break;
            }
            return Task.CompletedTask;
        }

        public static string MessageLog(MessageCreateEventArgs e,  string channelType)
        {
            return $"[{DateTime.Now.ToLongDateString()} : {DateTime.Now.ToShortTimeString()}] {e.Author.Username}#{e.Author.Discriminator} " +
                   $"| Message: \"{e.Message.Content}\" | {channelType} \n"; // Template for our logged message
        }
    }
}