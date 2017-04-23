using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdunnSql.Core.Model;

namespace IdunnSql.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<GenerateOptions>(args);
            var exitCode = result.MapResult(
                o => { return Generate(o); },
                e => { return 1; }
            );

            return exitCode;
        }

        protected static int Generate(GenerateOptions options)
        {
            System.Console.WriteLine($"Generating file {options.Destination} based on {options.Source}.");
            var factory = new ModelFactory();
            var principal = factory.Instantiate(options.Source);
            
            return 0;
        }
    }
}
