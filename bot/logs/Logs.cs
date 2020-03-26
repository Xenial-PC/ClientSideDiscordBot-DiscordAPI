using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;

namespace ClientSideSelfBot
{
    public class Logs
    {
        public static Task CommandErroredAsync(CommandErrorEventArgs e)
        {
            ErrorAsync($"{e.Context.User.Username} failed to execute: {e.Command?.QualifiedName ?? "Unknown command"} and recieved error: {e.Exception.Message ?? "No message"}");
            return Task.CompletedTask;
        }

        public static Task CommandExecutedAsync(CommandExecutionEventArgs e)
        {
            PrintAsync($"{e.Context.User.Username} successfully executed: {e.Command.QualifiedName}");
            return Task.CompletedTask;
        }

        public static Task MessageRecivedAsync(MessageCreateEventArgs e)
        {
            if (!e.Author.IsBot && e.Author != e.Client.CurrentUser)
            {
                if (e.Message.MessageType == MessageType.Default)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[{DateTime.Now}] ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{e.Message.Author.Username}#{e.Message.Author.Discriminator} | ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"{e.Message.Content}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" | #{e.Message.Channel.Name} {e.Guild.Name}" + Environment.NewLine);
                    Console.ResetColor();
                }
            }

            return Task.CompletedTask;
        }

        public static Task HeartBeatRecivedAsync(HeartbeatEventArgs e)
        {
            PrintAsync($"Heartbeat Recived: {e.Ping}ms + {DateTime.Now}");
            return Task.CompletedTask;
        }

        public static Task PrintAsync(string message) // Makes the logs look nice
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{DateTime.Now}] ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(message + Environment.NewLine);
            return Task.CompletedTask;
        }

        public static Task ErrorAsync(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"[{DateTime.Now}] ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message + Environment.NewLine);
            return Task.CompletedTask;
        }
    }
}
