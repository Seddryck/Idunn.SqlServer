using Idunn.Console.Template.StringTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Execution
{
    class ExecutorContainer
    {
        protected IDictionary<Type, IExecutorFactory> factories = new Dictionary<Type, IExecutorFactory>();

        public void Initialize()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(currentPath, "Idunn.*.dll");
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                var potentials = assembly.GetTypes().Where(t => typeof(IExecutorFactory).IsAssignableFrom(t));
                if (potentials.Count() == 1)
                {
                    var factory = (IExecutorFactory)potentials.ElementAt(0).GetConstructor(new Type[0]).Invoke(new object[0]);
                    var type = factory.GetType().GetCustomAttribute<ExecutorAttribute>().Type;
                    factories.Add(type, factory);
                }
                else
                {
                    System.Console.BackgroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine($"Warning: the file {file} doesn't contain any parser. The types defined in this dll won't be loaded by Idunn and will be skipped.");
                    System.Console.ResetColor();
                }
            }
        }

        public IExecutorFactory Retrieve(Type type)
        {
            return factories[type];
        }
    }
}
