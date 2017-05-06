using Idunn.SqlServer.Console.Parser;
using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Parser.YamlParser
{
    public class ParserRegister : IParserRegister
    {
        private Dictionary<Type, object> parsers;
        private IRootParser rootParser;

        public void Initialize(IParserContainer container)
        {
            rootParser = new RootParser(container);

            parsers = new Dictionary<Type, object>();
            parsers.Add(typeof(Principal), new PrincipalParser(container));
            parsers.Add(typeof(Database), new DatabaseParser(container));
            parsers.Add(typeof(Permission), new PermissionParser(container));
            parsers.Add(typeof(Securable), new SecurableParser(container));
            container.Initialize(this);
        }

        public IReadOnlyDictionary<Type, object> GetParsers()
        {
            return parsers;
        }
        
        public IRootParser GetRootParser()
        {
            return rootParser;
        }
    }
}
