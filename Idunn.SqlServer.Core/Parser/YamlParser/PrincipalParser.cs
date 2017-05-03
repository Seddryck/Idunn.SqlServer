using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Core.Parser.YamlParser
{
    class PrincipalParser : AbstractParser<Principal>
    {
        public PrincipalParser(ParserFactory factory)
        : base(factory)
        {
        }

        public override Principal Parse(YamlNode node)
        {
            if (node.NodeType!=YamlNodeType.Mapping)
                throw new ArgumentException();

            var mapping = (YamlMappingNode)node;

            var name = GetValue(mapping, "name");

            var databases = ParseChildren<Database>(mapping, "database", "databases");
            var principal = new Principal(name, databases);
            return principal;

        }

    }
}
