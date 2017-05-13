using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Idunn.SqlServer.Console.Parser;
using Idunn.SqlServer.Console.Model;
using Idunn.SqlServer.Console.Template;
using Idunn.SqlServer.Console.Execution;

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
            var collection = modelFactory.Instantiate(options.Source, container);

            var types = collection.Select(o => o.GetType()).Distinct();

            //Render the template
            foreach (var type in types)
            {
                var templateContainer = new TemplateContainer();
                templateContainer.Initialize();
                var templateFactory = templateContainer.Retrieve(type);
                var engine = templateFactory.Instantiate(string.Empty, false, options.Template);

                var objects = collection.Where(o => o.GetType() == type);
                var text = engine.Execute(objects);
                File.WriteAllText(options.Destination, text);
            }

            return 0;
        }

        protected static int Execute(ExecuteOptions options)
        {
            System.Console.WriteLine($"Execute permissions' checks based on {options.Source}.");
            //Parse the model
            var container = new ParserContainer();
            container.Initialize(Path.GetExtension(options.Source));

            var modelFactory = new ModelFactory();
            var collection = modelFactory.Instantiate(options.Source, container);

            var types = collection.Select(o => o.GetType()).Distinct();

            //Execute the checks
            foreach (var type in types)
            {
                var executorContainer = new ExecutorContainer();
                executorContainer.Initialize();
                var executorFactory = executorContainer.Retrieve(type);

                var textWriters = new List<TextWriter>()
                {
                    System.Console.Out,
                };
                if (!string.IsNullOrEmpty(options.Output))
                    textWriters.Add(new StreamWriter(options.Output));

                var engine = executorFactory.Instantiate(textWriters);

                var objects = collection.Where(o => o.GetType() == type);
                engine.Execute(objects);
            }

            return 0;
        }
    }
}
