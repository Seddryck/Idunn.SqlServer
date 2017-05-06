using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Idunn.SqlServer.Console.Parser;
using Idunn.SqlServer.Console.Model;

namespace Idunn.SqlServer.Console
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
            var container = new ParserContainer();
            container.Initialize(Path.GetExtension(options.Source));

            var modelFactory = new ModelFactory();

            var principals = modelFactory.Instantiate(options.Source, container);
            //if (principals.Count() > 1 && !string.IsNullOrEmpty(options.Principal))
            //    throw new ArgumentException($"The file {options.Source} contains more than one principal. You cannot specify the principal on the command line arguments.");
            //else
            //    principals.ElementAt(0).Name = options.Principal;
            ////Render the template
            //var engineFactory = new StringTemplateEngineFactory();
            //var engine = engineFactory.Instantiate(options.Principal, true, options.Template);
            //var text = engine.Execute(principals);
            ////Persist the rendering
            //File.WriteAllText(options.Destination, text);

            return 0;
        }

        protected static int Execute(ExecuteOptions options)
        {
            //System.Console.WriteLine($"Execute permissions' checks based on {options.Source}.");
            ////Parse the model
            //var modelFactory = new ModelFactory();
            //var principals = modelFactory.Instantiate(options.Source);
            //if (principals.Count() > 1 && !string.IsNullOrEmpty(options.Principal))
            //    throw new ArgumentException($"The file {options.Source} contains more than one principal. You cannot specify the principal on the command line arguments.");
            //else
            //    principals.ElementAt(0).Name = options.Principal;
            ////Execute the checks
            //var executionEngineFactory = new ExecutionEngineFactory();
            //var engine = executionEngineFactory.Instantiate(System.Console.Out, options.Output);
            //engine.Execute(principals);
            
            return 0;
        }
    }
}
