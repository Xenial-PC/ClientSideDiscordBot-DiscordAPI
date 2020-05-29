using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

// ReSharper disable ExceptionNotDocumented

namespace ClientSideSelfBot.Bot.Roles
{
    public class RoleCreator
    {
        public Task CreateRoleFileAsync(List<string> path, string folderName)
        {
            if (Directory.Exists(folderName)) return Task.CompletedTask;
            Directory.CreateDirectory(folderName);
            path.ForEach(file => File.Create($"{folderName}/{file}"));
            return Task.CompletedTask;
        }

        public async void CreateAllPaths(List<string> roleList, string folderName = "Roles")
        { 
            await CreateRoleFileAsync(roleList, folderName).ConfigureAwait(false);
            await ReadRoleListAsync(roleList, folderName).ConfigureAwait(false);
        }

        public Task ReadRoleListAsync(List<string> roleList, string folderName)
        {
            try
            {
                roleList.ForEach(role =>
                {
                    foreach (var line in File.ReadAllLines($"{folderName}/{role}"))
                    {
                        switch (role)
                        {
                            case "Admin.txt":
                            {
                                //FormatMessage.FormatStringAdvanced(line);
                                RoleProperties.AdminList.Add(line);
                            }break;
                            case "Mod.txt":
                            {
                                //FormatMessage.FormatStringAdvanced(line);
                                RoleProperties.ModList.Add(line);
                            }break;
                            case "Trusted.txt":
                            {
                                //FormatMessage.FormatStringAdvanced(line);
                                RoleProperties.TrustedList.Add(line);
                            }break;
                            case "Temp.txt":
                            {
                                //FormatMessage.FormatStringAdvanced(line);
                                RoleProperties.TempList.Add(line);
                            }break;
                            case "Banned.txt":
                            {
                                //FormatMessage.FormatStringAdvanced(line);
                                RoleProperties.BannedList.Add(line);
                            }break;
                            default:
                                Console.WriteLine("Error Failed To Load Lists");
                                break;
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"FIRST TIME LOAD - {e.Data} : Completed Restarting");
                System.Threading.Thread.Sleep(5000);
                Process.Start("ClientSideSelfBot.exe");
                Environment.Exit(0);
            }
            return Task.CompletedTask;
        }
    }
}