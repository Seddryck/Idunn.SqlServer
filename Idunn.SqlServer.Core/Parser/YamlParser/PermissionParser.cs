using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Core.Parser.YamlParser
{
    class PermissionParser : AbstractParser<Permission>
    {
        public PermissionParser(ParserFactory factory)
            : base(factory)
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
