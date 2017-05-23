using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Parser
{
    class PrincipalParser : AbstractParser<Principal>
    {
        public PrincipalParser(IParserContainer container, IEngineParser engine)
            : base(container, engine)
        {
        }

        public override Principal Parse(object node)
        {
            if (!engine.IsValid(node, "principal"))
                throw new ArgumentException();

            var name = engine.GetValue(node, "name");

            var databases = ParseChildren<Database>(node, new[] { "database", "databases" });
            var principal = new Principal(name, databases);
            return principal;

        }
    }
}
