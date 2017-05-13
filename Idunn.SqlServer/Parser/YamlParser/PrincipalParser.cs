using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Parser.YamlParser
{
    class PrincipalParser : AbstractYamlParser<Principal>
    {
        public PrincipalParser(IParserContainer container)
            : base(container)
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
