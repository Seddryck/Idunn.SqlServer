using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Parser.YamlParser
{
    public class RootParser : IRootParser
    {
        protected readonly IParserContainer container;

        public RootParser(IParserContainer container)
        {
            this.container = container;
        }

        public IEnumerable<object> Parse(Stream stream)
        {
            YamlDocument yamlDoc = null;
            using (var streamReader = new StreamReader(stream))
            {
                var yamlStream = new YamlDotNet.RepresentationModel.YamlStream();
                yamlStream.Load(streamReader);
                yamlDoc = yamlStream.Documents[0];
            }

            var root = (yamlDoc.RootNode.AllNodes.ElementAt(0) as YamlMappingNode).Children.ElementAt(0);

            var principals = new List<Principal>();
            var parser = container.Retrieve<Principal>();

            if ((root.Key as YamlScalarNode).Value == "principal")
            {
                var principal = parser.Parse(root.Value);
                principals.Add(principal);
            }
            else if ((root.Key as YamlScalarNode).Value == "principals")
            {
                foreach (YamlNode child in (YamlSequenceNode)root.Value)
                    principals.Add(parser.Parse(child));
            }
            return principals;
        }
        
    }
}
