using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Parser
{
    public class ParserContainer : IParserContainer
    {
        private Dictionary<Type, object> parsers = new Dictionary<Type, object>();
        private IList<object> rootParsers = new List<object>();

        public void Initialize(string extension)
        {
            var factory = new EngineParserFactory();
            var engine = factory.Instantiate(extension);

            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(currentPath, "Idunn.*.Idunn.SqlServer.dll");
            var factories = new List<object>();
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                var potentials = assembly.GetTypes().Where(t => typeof(IParserRegister).IsAssignableFrom(t));
                if (potentials.Count() == 1)
                {
                    var effective = potentials.Single();
                    var register = effective.GetConstructor(new Type[0]).Invoke(new object[0]);
                    Initialize(register as IParserRegister, engine);
                }
                else
                {
                    System.Console.BackgroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine($"Warning: the file {file} doesn't contain any parser. The types defined in this dll won't be loaded by Idunn and will be skipped.");
                    System.Console.ResetColor();
                }
            }
        }

        public void Initialize(IParserRegister register, string extension)
        {
            var factory = new EngineParserFactory();
            Initialize(register, factory.Instantiate(extension));
        }

        public void Initialize(IParserRegister register, IEngineParser engine)
        {
            register.Initialize(this, engine);
            rootParsers.Add(register.GetRootParser());
            foreach (var item in register.GetParsers())
                parsers.Add(item.Key, item.Value);              
        }

        public IParser Retrieve<T>()
        {
            if (parsers.Keys.Contains(typeof(T)))
                return parsers[typeof(T)] as IParser;
            throw new ArgumentException();
        }

        public IEnumerable<object> RootParsers
        {
            get { return rootParsers;}
        }
    }
}
