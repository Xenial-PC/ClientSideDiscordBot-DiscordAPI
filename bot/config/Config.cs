using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClientSideSelfBot
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
                        Bot = "",

                        Admin = new List<string>
                        {
                            "",
                            ""
                        },
                        Mod = new List<string>
                        {
                            "",
                            ""
                        },
                        Trusted = new List<string>
                        {
                            "",
                            ""
                        },
                        Temp = new List<string>
                        {
                            "",
                            ""
                        }
                    };
                    var serializedJson = JsonConvert.SerializeObject(configJson, Formatting.Indented); // Formats the file to json configurations
                    File.WriteAllText("config.json", serializedJson); // writes all text

                    Console.WriteLine("Put your token in config.json, Then start the program! And make sure to put the user IDS in the config file aswell");
                    Console.ReadKey();

                    Environment.Exit(0);
                }

                var json = "";
                using (var config = File.OpenRead("config.json"))
                {
                    using (var sr = new StreamReader(config, new UTF8Encoding(false)))
                    {
                        json = sr.ReadToEnd();
                    }
                }
                var cfgJson = JsonConvert.DeserializeObject<ConfigJson>(json);

                Program.token = cfgJson.Token;
                Program.selfbot = cfgJson.SelfBot;
                Program.prefix = cfgJson.Prefix;
                Program.owner = cfgJson.Owner;
                Program.bot = cfgJson.Bot;
                Program.admin = cfgJson.Admin;
                Program.mod = cfgJson.Mod;
                Program.trusted = cfgJson.Trusted;
                Program.temp = cfgJson.Temp;
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
