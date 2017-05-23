using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YamlDotNet.RepresentationModel;

namespace Idunn.Console.Parser
{
    public class YamlEngineParser : IEngineParser
    {

        public bool IsValid(object node, string name)
        {
            if (node is KeyValuePair<YamlNode, YamlNode> kvp)
                return ((YamlScalarNode)(kvp.Key)).Value == name;
            if (node is YamlMappingNode yamlMappingNode)
                return yamlMappingNode.NodeType == YamlNodeType.Mapping;
            
            return (node is YamlScalarNode);
        }

        public string GetValue(object node, string name)
        {
            if (node is YamlScalarNode)
                return ((YamlScalarNode)node).Value;

            YamlMappingNode yamlMappingNode = null;
            if (node is KeyValuePair<YamlNode, YamlNode>)
                yamlMappingNode = (YamlMappingNode)((KeyValuePair<YamlNode, YamlNode>)node).Value;
            else if (node is YamlMappingNode)
                yamlMappingNode = (YamlMappingNode)node;

            if (yamlMappingNode.Children.ContainsKey(new YamlScalarNode(name)))
                return (yamlMappingNode.Children[new YamlScalarNode(name)] as YamlScalarNode)?.Value;

            return string.Empty;
        }

        public IEnumerable<object> GetChildren(object node, string[] names)
        {
            YamlMappingNode yamlMappingNode = null;
            if (node is KeyValuePair<YamlNode, YamlNode>)
                yamlMappingNode = (YamlMappingNode)((KeyValuePair<YamlNode, YamlNode>)node).Value;
            else
                yamlMappingNode = (YamlMappingNode)node;

            var children = new List<YamlNode>();
            foreach (var name in names)
            {
                if (yamlMappingNode.Children.ContainsKey(new YamlScalarNode(name)))
                {
                    var nodes = yamlMappingNode.Children[new YamlScalarNode(name)];
                    if (nodes is YamlSequenceNode)
                        foreach (YamlNode child in (YamlSequenceNode)nodes)
                            children.Add(child);
                    else
                        children.Add(nodes);
                }
            }
            
            return children;
        }

        public object InstantiateReader(StreamReader streamReader)
        {
            var yamlStream = new YamlStream();
            yamlStream.Load(streamReader);
            return yamlStream;
        }

        public object GetRoot(object reader)
        {
            if (!(reader is YamlStream))
                throw new ArgumentException();
            var yamlStream = (YamlStream)reader;

            var yamlDoc = yamlStream.Documents[0];
            var root = (yamlDoc.RootNode.AllNodes.ElementAt(0) as YamlMappingNode).Children.ElementAt(0);
            return root;
        }

        public bool IsRootName(object root, string[] names)
        {
            if (!(root is KeyValuePair<YamlNode, YamlNode>))
                throw new ArgumentException();

            var rootNode = (KeyValuePair<YamlNode, YamlNode>)root;
            var name = (rootNode.Key as YamlScalarNode).Value;
            return names.Contains(name);
        }
    }
}
