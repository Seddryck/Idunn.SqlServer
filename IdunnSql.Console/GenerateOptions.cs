using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Console
{
    [Verb("generate", HelpText = "Generate a file")]
    public class GenerateOptions
    {
        [Option('s', "source", Required = true,
        HelpText = "Name of the file containing information about the permissions to check")]
        public string Source { get; set; }

        [Option('d', "destination", Required = true,
        HelpText = "Name of the file to be generated.")]
        public string Destination { get; set; }
    }
}
