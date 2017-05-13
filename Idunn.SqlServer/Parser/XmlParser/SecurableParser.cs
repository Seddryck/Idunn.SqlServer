using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Parser.XmlParser
{
    class SecurableParser : AbstractXmlParser<Securable>
    {
        public SecurableParser(IParserContainer container)
            : base(container)
        {
        }

        public override Securable Parse(XmlNode node)
        {
            if (node.Name != "securable")
                throw new ArgumentException();

            var name = node.Attributes["name"]?.Value;
            var type = node.Attributes["type"]?.Value;

            var permissions = ParseChildren<Permission>(node, "permission");

            var securable = new Securable(name, type, permissions);
            return securable;

        }
    }
}
