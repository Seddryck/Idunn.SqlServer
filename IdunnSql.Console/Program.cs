using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdunnSql.Core.Model;
using IdunnSql.Core.Template.StringTemplate;
using System.IO;
using IdunnSql.Core.Execution;

namespace IdunnSql.Console
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<GenerateOptions, ExecuteOptions>(args);
            var exitCode = result.MapResult(
                (GenerateOptions o) => { return Generate(o); },
                (ExecuteOptions o) => { return Execute(o); },
                error => { return 1; }
            );

            return exitCode;
        }

        protected static int Generate(GenerateOptions options)
        {
            System.Console.WriteLine($"Generating file {options.Destination} based on {options.Source}.");
            //Parse the model
            var factory = new ModelFactory();
            var principal = factory.Instantiate(options.Source);
            principal.Name = options.Principal;
            //Render the template
            var engineFactory = new StringTemplateEngineFactory();
            var engine = engineFactory.Instantiate(options.Principal, true, options.Template);
            var text = engine.Execute(principal);
            //Persist the rendering
            File.WriteAllText(options.Destination, text);

            return 0;
        }

        protected static int Execute(ExecuteOptions options)
        {
            System.Console.WriteLine($"Execute permissions' checks based on {options.Source}.");
            //Parse the model
            var modelFactory = new ModelFactory();
            var principal = modelFactory.Instantiate(options.Source);
            principal.Name = options.Principal;
            //Eexcute the checks
            var executionEngineFactory = new ExecutionEngineFactory();
            var engine = executionEngineFactory.Instantiate(System.Console.Out, options.Output);
            engine.Execute(principal);
            
            return 0;
        }
    }
}
