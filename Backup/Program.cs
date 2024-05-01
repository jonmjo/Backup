using Backup.Helpers;
using CommandLine;

namespace Backup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.OpenStandardError().;
            object? oo = null;
            var cc = Parser.Default.ParseArguments<CmdVerb>(args)
                    .WithParsed<CmdVerb>(options => BackupCommand.Run(options))
                    .WithNotParsed(errors => oo = errors)
                ;


        }
    }
}
