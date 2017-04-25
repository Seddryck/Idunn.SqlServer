using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Console
{
    [Verb("execute", HelpText = "Execute the permission checks defined in a file")]
    public class ExecuteOptions
    {
        [Option('s', "source", Required = true,
        HelpText = "Name of the file containing information about the permissions to check")]
        public string Source { get; set; }
        
        [Option('p', "principal", Required = false,
        HelpText = "Name of the principal to impersonate.")]
        public string Principal { get; set; }

        [Option('o', "output", Required = false,
        HelpText = "Name of the file to redirect the output of the console.")]
        public string Output { get; set; }
    }
}
