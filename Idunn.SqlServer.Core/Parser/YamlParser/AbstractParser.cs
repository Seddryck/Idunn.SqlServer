using Idunn.SqlServer.Console.Parser;
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
        protected readonly IParserContainer container;

        public AbstractParser(IParserContainer container)
        {
            this.container = container;
        }

        public T Parse(object node)
        {
            if (!(node is YamlNode))
                throw new ArgumentException();
            return Parse((YamlNode)node);
        }

        public abstract T Parse(YamlNode node);

        protected List<S> ParseChildren<S>(YamlMappingNode node, string single, string multiple)
        {
            var parser = container.Retrieve<S>();
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
