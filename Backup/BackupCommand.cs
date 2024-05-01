using Backup.Helpers;
using Backup.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Backup.Commands;

namespace Backup
{
    public class BackupCommand
    {
        internal static void Run(CmdVerb options)
        {
            if (options.Create)
            {
                ConfigFile.CreateConfigFile(options);
            }
            else if (options.Run)
            {
                Runs.BackupCommands(options);
            }
            else
            {
                Console.WriteLine("What todo, I don't know.");
                Console.WriteLine("¯\\_(ツ)_/¯");
            }
        }


    }
}
