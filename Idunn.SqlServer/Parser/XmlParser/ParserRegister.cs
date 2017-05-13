using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Parser.XmlParser
{
    [FileParser(".xml")]
    public class ParserRegister : IParserRegister
    {
        private Dictionary<Type, object> parsers;
        private IRootParser rootParser;

        public void Initialize(IParserContainer container)
        {
            rootParser = new RootParser(container);

            parsers = new Dictionary<Type, object>()
            {
                [typeof(Principal)] = new PrincipalParser(container),
                [typeof(Database)] = new DatabaseParser(container),
                [typeof(Permission)] = new PermissionParser(container),
                [typeof(Securable)] = new SecurableParser(container),
            };
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
