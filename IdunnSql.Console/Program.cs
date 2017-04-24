using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdunnSql.Core.Model;
using IdunnSql.Core.Template.StringTemplate;
using System.IO;

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
            //Parse the model
            var factory = new ModelFactory();
            var principal = factory.Instantiate(options.Source);
            //Render the template
            var engine = new CurrentUserEngine();
            var text = engine.Execute(principal);
            //Persist the rendering
            File.WriteAllText(options.Destination, text);

            return 0;
        }
    }
}
