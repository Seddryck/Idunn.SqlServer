using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.Console.Parser
{
    public class XmlEngineParser : IEngineParser
    {
        public bool IsValid(object node, string name)
        {
            if (!(node is XmlNode))
                throw new ArgumentException();

            return ((node as XmlNode).Name == name);
        }

        public string GetValue(object node, string name)
        {
            if (!(node is XmlNode))
                throw new ArgumentException();

            return (node as XmlNode).Attributes[name]?.Value;
        }

        public IEnumerable<object> GetChildren(object node, string[] names)
        {
            if (!(node is XmlNode))
                throw new ArgumentException();

            return (node as XmlNode).ChildNodes.Cast<XmlNode>().Where(n => names.Contains(n.Name));
        }

        public object InstantiateReader(StreamReader streamReader)
        {
            var xmlReaderFactory = new XmlReaderFactory();
            return xmlReaderFactory.Instantiate(streamReader);
        }

        public object GetRoot(object reader)
        {
            if (!(reader is XmlReader))
                throw new ArgumentException();

            var xmlReader = (XmlReader)reader;

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlReader);
            var root = xmlDoc.FirstChild.NextSibling;
            return root;
        }

        public bool IsRootName(object root, string[] names)
        {
            if (!(root is XmlNode))
                throw new ArgumentException();

            var rootNode = (XmlNode)root;
            return names.Contains(rootNode.Name);

        }
    }
}
