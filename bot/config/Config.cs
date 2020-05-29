using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
// ReSharper disable CatchAllClause
// ReSharper disable ExceptionNotDocumented

namespace ClientSideSelfBot.Bot.Config
{
    public class Config
    {
        public static void LoadConfig()
        {
            try
            {
                if (!File.Exists("config.json")) // If the json file does not exist it will make a new one
                {
                    var configJson = new ConfigJson
                    {
                        Token = "",
                        SelfBot = false,
                        Prefix = ";",
                        Owner = "",
                        Bot = ""
                    };
                    var serializedJson = JsonConvert.SerializeObject(configJson, Formatting.Indented); // Formats the file to json configurations
                    File.WriteAllText("config.json", serializedJson); // writes all text
                    Console.WriteLine("Put your token in config.json, Then start the program! And make sure to put the user IDS in the config file aswell");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                string json;
                using (var config = File.OpenRead("config.json")) 
                using (var sr = new StreamReader(config, new UTF8Encoding(false))) json = sr.ReadToEnd();
                var cfgJson = JsonConvert.DeserializeObject<ConfigJson>(json);
                Program.Token = cfgJson.Token;
                Program.Selfbot = cfgJson.SelfBot;
                Program.Prefix = cfgJson.Prefix;
                Program.Owner = cfgJson.Owner;
                Program.Bot = cfgJson.Bot;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}