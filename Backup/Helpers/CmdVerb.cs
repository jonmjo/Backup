using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Helpers
{
    [Verb("run", HelpText = "Runs selected backup command")]
    public class CmdVerb
    {
        /// <summary>
        /// Path to config file. e.g. C:\MoveOn\moveonconfig_prod.json
        /// </summary>
        [Option('F', "filename", Required = true, HelpText = "Configuration file.")]
        public string ConfigFilePath { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Option('C', "create", Required = false, HelpText = "Creates an empty configuration file.")]
        public bool Create { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Option('r', "run", Required = false, HelpText = "Runs commands in config file.")]
        public bool Run { get; set; }
    }
}
