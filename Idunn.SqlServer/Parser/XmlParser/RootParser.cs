using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Parser.XmlParser
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
            using (var streamReader = new StreamReader(stream))
            {
                var xmlReaderFactory = new XmlReaderFactory();
                using (var xmlReader = xmlReaderFactory.Instantiate(streamReader))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlReader);

                    var root = xmlDoc.FirstChild.NextSibling;

                    var principals = new List<Principal>();
                    var parser = container.Retrieve<Principal>();
                    if (root.Name=="principal")
                    {
                        var principal = parser.Parse(root);
                        principals.Add(principal);
                    }
                    else if (root.Name == "idunn")
                    {
                        foreach (XmlNode child in root.ChildNodes.Cast<XmlNode>().Where(n => n.Name == "principal"))
                        {
                            var principal = parser.Parse(child);
                            principals.Add(principal);
                        }
                    }
                    return principals;
                }
            }
        }
    }
}
