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
    class PermissionParser : AbstractParser<Permission>
    {
        public PermissionParser(IParserContainer container, IEngineParser engine)
            : base(container, engine)
        {
        }

        public override Permission Parse(object node)
        {
            if (!engine.IsValid(node, "permission"))
                throw new ArgumentException();

            var name = engine.GetValue(node, "name");

            return new Permission(name);
        }
    }
}
