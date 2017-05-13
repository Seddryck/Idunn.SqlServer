using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Parser.YamlParser
{
    class PermissionParser : AbstractParser<Permission>
    {
        public PermissionParser(IParserContainer container)
            : base(container)
        {
        }

        public override Permission Parse(YamlNode node)
        {
            if (node.NodeType != YamlNodeType.Scalar)
                throw new ArgumentException();

            var scalar = (YamlScalarNode)node;

            var name = scalar.Value;

            return new Permission(name);
        }
    }
}
