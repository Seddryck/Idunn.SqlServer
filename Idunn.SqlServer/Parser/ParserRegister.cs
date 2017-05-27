using Idunn.Core.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Parser
{
    public class ParserRegister : IParserRegister
    {
        private Dictionary<Type, object> parsers;
        private IRootParser rootParser;

        public void Initialize(IParserContainer container, IEngineParser engine)
        {
            rootParser = new RootParser(container, engine);

            parsers = new Dictionary<Type, object>()
            {
                [typeof(Principal)] = new PrincipalParser(container, engine),
                [typeof(Database)] = new DatabaseParser(container, engine),
                [typeof(Permission)] = new PermissionParser(container, engine),
                [typeof(Securable)] = new SecurableParser(container, engine),
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
