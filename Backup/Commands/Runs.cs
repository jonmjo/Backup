using Backup.Helpers;
using Backup.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Commands
{
    internal class Runs
    {
        public static void BackupCommands(CmdVerb options)
        {
            ConfigFile configFile = ConfigFile.Get(options.ConfigFilePath);

            foreach (var item in configFile.List.OrderBy(x=>x.Order))
            {
                if (item.IsActive == false)
                {
                    Console.WriteLine("Not active in config file. Skipping: " + item.Name);
                    continue;
                }

                Console.WriteLine("Processing: " + item.Name);

                foreach (var command in item.Commands.OrderBy(X => X.Order))
                {
                    if (command.IsActive == false)
                    {
                        Console.WriteLine("Command not active in config file. Skipping: " + command.ExecutableFileName);
                        continue;
                    }

                    var startInfo = new ProcessStartInfo();

                    var args = command.Arguments.Replace("{datetime}", DateTime.Now.ToString("yyyy-MM-dd_HHmmss")) ?? "";
                    Console.WriteLine($"- Running: {command.ExecutableFileName} {args}");

                    startInfo.FileName = command.ExecutableFileName;
                    foreach (var arg in args.Split(' '))
                    {
                        startInfo.ArgumentList.Add(arg);
                    }
                    foreach (var key in item.EnvironmentVariables.Keys)
                    {
                        if (startInfo.EnvironmentVariables.ContainsKey(key))
                            startInfo.EnvironmentVariables[key] = item.EnvironmentVariables[key];
                        else
                            startInfo.EnvironmentVariables.Add(key, item.EnvironmentVariables[key]);
                    }

                    var p = Process.Start(startInfo);
                    p?.WaitForExit();
                }
                Console.WriteLine("Done with: " + item.Name);
            }
        }
    }
}
