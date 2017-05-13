using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using Idunn.SqlServer.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Parser.YamlParser
{
    class DatabaseParser : AbstractYamlParser<Database>
    {
        public DatabaseParser(IParserContainer container)
            : base(container)
        {
        }

        public override Database Parse(YamlNode node)
        {
            if (node.NodeType != YamlNodeType.Mapping)
                throw new ArgumentException();

            var mapping = (YamlMappingNode)node;

            var name = GetValue(mapping, "name");
            var server = GetValue(mapping, "server");

            var securables = ParseChildren<Securable>(mapping, "securable", "securables");
            var permissions = ParseChildren<Permission>(mapping, "permission", "permissions");

            var database = new Database(name, server, securables, permissions);
            
            return database;
        }

    }
}
