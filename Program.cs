using ClientSideSelfBot.bot.logs;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClientSideSelfBot
{
    public class Program
    {
        public static DiscordClient discordClient;
        public static CommandsNextModule commandsNext;

        public static string token = "";
        public static string owner = "";
        public static string bot = "";
        public static string prefix = "";

        public static List<string> admin = new List<string>();
        public static List<string> mod = new List<string>();
        public static List<string> trusted = new List<string>();
        public static List<string> temp = new List<string>();

        public static bool selfbot;
        readonly bool disposed;

        static void Main()
        {
            Config.LoadConfig(); // loads the config
            AdvancedLogger.LoadAdvancedLoggerConfig(); // Loads the AdvancedLogger config

            var start = new Program(); // this is just for ese of acess
            start.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult(); // runs the start task, configured await to be false, gets the awaiter in the Task and then returns the result
        }

        async Task StartAsync()
        {
            try
            {
                var clientConfig = new DiscordConfiguration
                {
                    LogLevel = LogLevel.Critical, // makes message logging to critical so you can collect all the info
                    Token = token, // sets the token from config.json
                    TokenType = TokenType.User, // sets the bots token type to user if your using a user account
                    UseInternalLogHandler = true // this is set to true so we can use our own message logger
                };
                var commandConfig = new CommandsNextConfiguration
                {
                    StringPrefix = prefix, // Prefix for the bot
                    EnableDefaultHelp = false, // removed the default help command so we can make our own
                    SelfBot = selfbot, // turn this to true if your using this as a self bot only also configureable in config.json
                    CaseSensitive = false
                };
                discordClient = new DiscordClient(clientConfig); // makes a new discord client and uses our client config

                commandsNext = discordClient.UseCommandsNext(commandConfig); // sets the default command route to our command file
                commandsNext.RegisterCommands<Commands>(); // registers the commands

                discordClient.Heartbeated += Logs.HeartBeatRecivedAsync; // gets the heartbeat from the logs
                discordClient.MessageCreated += Logs.MessageRecivedAsync; // gets the messages recived from the logs

                commandsNext.CommandErrored += Logs.CommandErroredAsync; // gets the errored commands from the logs
                commandsNext.CommandExecuted += Logs.CommandExecutedAsync; // gts the executed commands from the logs

                await AdvancedLogger.MessageLoggerAsync(); // Logs the new messages in a log file
                CustomCommands.Custom(); // gets our custom commands from the logs

                await discordClient.ConnectAsync(); // await the connection from the discord client
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
            await Task.Delay(-1);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                StartAsync().Dispose();
            }
        }
    }
}
