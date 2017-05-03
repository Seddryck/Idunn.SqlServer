using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.RepresentationModel;

namespace Idunn.SqlServer.Core.Parser.YamlParser
{
    public abstract class AbstractParser<T> : IParser<T>
    {
        protected readonly ParserFactory factory;

        public AbstractParser(ParserFactory factory)
        {
            this.factory = factory;
        }

        public abstract T Parse(YamlNode node);

        protected List<S> ParseChildren<S>(YamlMappingNode node, string single, string multiple)
        {
            var parser = factory.Retrieve<S>();
            var children = new List<S>();
            if (node.Children.ContainsKey(new YamlScalarNode(single)))
            {
                var child = node.Children[new YamlScalarNode(single)];
                children.Add(parser.Parse(child));
            }
            if (node.Children.ContainsKey(new YamlScalarNode(multiple)))
            {
                foreach (YamlNode child in (YamlSequenceNode)node.Children[new YamlScalarNode(multiple)])
                    children.Add(parser.Parse(child));
            }
            return children;
        }


        protected string GetValue(YamlMappingNode node, string attribute)
        {
            if (node.Children.ContainsKey(new YamlScalarNode(attribute)))
                return (node.Children[new YamlScalarNode(attribute)] as YamlScalarNode)?.Value;
            else
                return string.Empty;
        }
    }
}
