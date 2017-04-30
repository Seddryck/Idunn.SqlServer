using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IdunnSql.Core.Parser.XmlParser
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
            using (var streamReader = new StreamReader(stream))
            {
                var xmlReaderFactory = new XmlReaderFactory();
                using (var xmlReader = xmlReaderFactory.Instantiate(streamReader))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlReader);

                    var root = xmlDoc.FirstChild.NextSibling;

                    var principals = new List<Principal>();
                    var parser = factory.Retrieve<Principal>();
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
