using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
// ReSharper disable ExceptionNotDocumented

namespace ClientSideSelfBot.Bot.Commands
{
    public class Roast
    {
        public const string Path = @"roastLists/roastList.txt";
        public static List<string> Roasts = new List<string>();
        public static Task InstallRoastList()
        {
            try
            {
                if (!File.Exists(Path))
                {
                    Directory.CreateDirectory("roastLists");
                    const string downloadLink = @"https://doc-0g-2g-docs.googleusercontent.com/docs/securesc/mq8qj5pje2qdpj8pobmf9m6cuju0c4mv/kp1sir105i9chnpsjpqcav4b2pl9lq8q/1585621575000/05737478279032310284/16094319650687264262/14h9wh6UlPhYFumEVboXmTLPP4fs-hGOT?e=download&authuser=0&nonce=3is2vcgoe48l8&user=16094319650687264262&hash=atra339qvl8fbbp75lhqvkj9mrcu4ocj";
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(downloadLink, "roastList.txt");
                        File.Move("roastList.txt", Path);
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

        public static Task FillRoastListAsync(string path = Path)
        {
            var roasts = File.ReadAllLines(path);
            foreach (var roast in roasts) Roasts.Add(roast);
            return Task.CompletedTask;
        }
    }
}
