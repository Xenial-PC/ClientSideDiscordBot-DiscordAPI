using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientSideSelfBot.bot.commands
{
    public class Roast
    {
        public const string _path = @"roastLists/roastList.txt";
        public static List<string> _roasts = new List<string>();

        public static Task InstallRoastList()
        {
            try
            {
                if (!File.Exists(_path))
                {
                    Directory.CreateDirectory("roastLists");
                    const string downloadLink = @"https://doc-0g-2g-docs.googleusercontent.com/docs/securesc/mq8qj5pje2qdpj8pobmf9m6cuju0c4mv/kp1sir105i9chnpsjpqcav4b2pl9lq8q/1585621575000/05737478279032310284/16094319650687264262/14h9wh6UlPhYFumEVboXmTLPP4fs-hGOT?e=download&authuser=0&nonce=3is2vcgoe48l8&user=16094319650687264262&hash=atra339qvl8fbbp75lhqvkj9mrcu4ocj";
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(downloadLink, "roastList.txt");
                        File.Move("roastList.txt", _path);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }

            return Task.CompletedTask;
        }

        public static Task FillRoastList(string path = _path)
        {
            var roasts = File.ReadAllLines(path);
            foreach (string roast in roasts)
            {
                _roasts.Add(roast);
            }

            return Task.CompletedTask;
        }
    }
}
