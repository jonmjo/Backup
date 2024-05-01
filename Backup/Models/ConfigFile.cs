using Backup.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Models
{
    public class Command
    {
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public string ExecutableFileName { get; set; } = string.Empty;
        public string Arguments { get; set; } = string.Empty;
    }
    public class BackupItem
    {
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string,string> EnvironmentVariables { get; set; } = new Dictionary<string,string>();
        public List<Command> Commands { get; set; } = [];


    }
    public class ConfigFile
    {
        public List<BackupItem> List { get; set; } = [];

        internal static ConfigFile Get(string configFilePath)
        {
            var fileContent = File.ReadAllText(configFilePath);
            var configFile = JsonConvert.DeserializeObject<ConfigFile>(fileContent)
                ?? new ConfigFile();
            return configFile;
        }


        public static void CreateConfigFile(CmdVerb options)
        {
            if (File.Exists(options.ConfigFilePath))
            {
                Console.WriteLine($"File already exists {options.ConfigFilePath}.");
                return;
            }

            var cf = new ConfigFile();
            cf.List.Add(new BackupItem());
            cf.List.First().Commands.Add(new Command());
            cf.List.First().EnvironmentVariables.Add("PGPASSWORD", "123");
            cf.List.First().EnvironmentVariables.Add("BLABLA", "123");
            var fc = JsonConvert.SerializeObject(cf, Formatting.Indented);
            File.WriteAllText(options.ConfigFilePath, fc);
            Console.WriteLine("Created example file.");
        }
    }

}
