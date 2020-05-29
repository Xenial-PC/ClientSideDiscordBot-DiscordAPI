using ClientSideSelfBot.Bot.Commands;
using ClientSideSelfBot.Bot.Logs;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientSideSelfBot.Bot.Config;
using ClientSideSelfBot.Bot.Roles;

// ReSharper disable CatchAllClause

namespace ClientSideSelfBot
{
    public class Program
    {
        public static DiscordClient DiscordClient;
        public static CommandsNextModule CommandsNext;
        public static string Token = "", Owner = "", Bot = "", Prefix = "";
        static readonly List<string> RoleList = new List<string> { "Admin.txt", "Mod.txt", "Trusted.txt", "Temp.txt", "Banned.txt" };
        public static bool Selfbot;

        static async Task Main()
        {
            Config.LoadConfig(); // loads the config
            AdvancedLogger.LoadAdvancedLoggerConfig(); // Loads the AdvancedLogger config
            await Roast.InstallRoastList().ConfigureAwait(false); // Installs the default roast list
            await Roast.FillRoastListAsync().ConfigureAwait(false); // fills the roast list with the default roasts
            new RoleCreator().CreateAllPaths(RoleList);
            StartAsync().ConfigureAwait(false).GetAwaiter().GetResult(); // runs the start task, configured await to be false, gets the awaiter in the Task and then returns the result
        }

        private static async Task StartAsync()
        {
            try
            {
                var clientConfig = new DiscordConfiguration
                {
                    LogLevel = LogLevel.Critical, // makes message logging to critical so you can collect all the info
                    Token = Token, // sets the token from config.json
                    TokenType = TokenType.User, // sets the bots token type to user if your using a user account
                    UseInternalLogHandler = true // this is set to true so we can use our own message logger
                };
                var commandConfig = new CommandsNextConfiguration
                {
                    StringPrefix = Prefix, // Prefix for the bot
                    EnableDefaultHelp = false, // removed the default help command so we can make our own
                    SelfBot = Selfbot, // turn this to true if your using this as a self bot only also configurable in config.json
                    CaseSensitive = false
                };
                DiscordClient = new DiscordClient(clientConfig); // makes a new discord client and uses our client config
                CommandsNext = DiscordClient.UseCommandsNext(commandConfig); // sets the default command route to our command file
                DiscordClient.Heartbeated += Logs.HeartBeatReceivedAsync; // gets the heartbeat from the logs
                DiscordClient.MessageCreated += Logs.MessageRecivedAsync; // gets the messages received from the logs

                await AdvancedLogger.MessageLoggerAsync().ConfigureAwait(false); // Logs the new messages in a log file
                Commands.Custom(); // gets our custom commands from the logs
                await DiscordClient.ConnectAsync().ConfigureAwait(false); // await the connection from the discord client
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
            await Task.Delay(-1).ConfigureAwait(false);
        }
    }
}