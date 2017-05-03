using Idunn.SqlServer.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Core.Parser.YamlParser
{
    public class RootParser
    {
        protected readonly ParserFactory factory;

        public RootParser(ParserFactory factory)
        {
            this.factory = factory;
        }

        public IEnumerable<Principal> Parse(Stream stream)
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
            var parser = factory.Retrieve<Principal>();

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
