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
    class SecurableParser : AbstractParser<Securable>
    {
        public SecurableParser(IParserContainer container, IEngineParser engine)
            : base(container, engine)
        {
        }

        public override Securable Parse(object node)
        {
            if (!engine.IsValid(node, "securable"))
                throw new ArgumentException();

            var name = engine.GetValue(node, "name");
            var type = engine.GetValue(node, "type");

            var permissions = ParseChildren<Permission>(node, new[] { "permission", "permissions" });

            var securable = new Securable(name, type, permissions);
            return securable;

        }
    }
}
