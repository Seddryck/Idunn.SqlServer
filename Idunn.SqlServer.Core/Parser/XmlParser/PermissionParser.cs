using Idunn.SqlServer.Console.Parser;
using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Core.Parser.XmlParser
{
    class PermissionParser : AbstractParser<Permission>
    {
        public PermissionParser(IParserContainer container)
            : base(container)
        {
        }

        public override Permission Parse(XmlNode node)
        {
            if (node.Name != "permission")
                throw new ArgumentException();

            var name = node.Attributes["name"]?.Value;

            return new Permission(name);
        }
    }
}
