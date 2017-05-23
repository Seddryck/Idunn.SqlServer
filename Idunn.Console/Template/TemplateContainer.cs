using Idunn.Console.Template.StringTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Template
{
    class TemplateContainer
    {
        protected IDictionary<Type, ITemplateFactory> factories = new Dictionary<Type, ITemplateFactory>();

        public void Initialize()
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(currentPath, "Idunn.*.dll");
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                var potentials = assembly.GetTypes().Where(t => typeof(ITemplateFactory).IsAssignableFrom(t));
                if (potentials.Count() == 1)
                {
                    var factory = (ITemplateFactory)potentials.ElementAt(0).GetConstructor(new Type[0]).Invoke(new object[0]);

                    factories.Add(factory.GetRootObjectType(), factory);
                }
                else
                {
                    System.Console.BackgroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine($"Warning: the file {file} doesn't contain any parser. The types defined in this dll won't be loaded by Idunn and will be skipped.");
                    System.Console.ResetColor();
                }
            }
        }

        public ITemplateFactory Retrieve(Type type)
        {
            return factories[type];
        }
    }
}
