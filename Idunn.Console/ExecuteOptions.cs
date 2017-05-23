using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console
{
    [Verb("execute", HelpText = "Execute the permission checks and report the results.")]
    public class ExecuteOptions
    {
        [Option('s', "source", Required = true,
        HelpText = "Name of the file containing information about the permissions to check.")]
        public string Source { get; set; }

        [Option('o', "output", Required = false,
        HelpText = "Name of the file to redirect the output of the execution.")]
        public string Output { get; set; }
    }
}
