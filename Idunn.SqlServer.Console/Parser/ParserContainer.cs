using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Console.Parser
{
    public class ParserContainer : IParserContainer
    {
        private Dictionary<Type, object> parsers = new Dictionary<Type, object>();
        private IList<IRootParser> rootParsers = new List<IRootParser>();

        public void Initialize(string extension)
        {
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(currentPath, "Idunn.*.dll");
            var factories = new List<object>();
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                var potentials = assembly.GetTypes().Where(t => typeof(IParserRegister).IsAssignableFrom(t));
                if (potentials.Count() > 0)
                {
                    var effective = potentials.Single(t => t.GetCustomAttribute<FileParserAttribute>().Extension == extension);
                    var register = effective.GetConstructor(new Type[0]).Invoke(new object[0]);
                    Initialize(register as IParserRegister);
                }
                else
                {
                    System.Console.BackgroundColor = ConsoleColor.Yellow;
                    System.Console.WriteLine($"Warning: the file {file} doesn't contain any parser. The types defined in this dll won't be loaded by Idunn and will be skipped.");
                    System.Console.ResetColor();
                }
            }
        }

        public void Initialize(IParserRegister register)
        {
            register.Initialize(this);
            rootParsers.Add(register.GetRootParser());
            foreach (var item in register.GetParsers())
            {
                parsers.Add(item.Key, item.Value);
            }
                
        }

        public IParser<T> Retrieve<T>()
        {
            if (parsers.Keys.Contains(typeof(T)))
                return parsers[typeof(T)] as IParser<T>;
            throw new ArgumentException();
        }

        public IEnumerable<IRootParser> RootParsers
        {
            get { return rootParsers;}
        }
    }
}
