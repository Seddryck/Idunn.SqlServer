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
    public class Parser
    {
        protected readonly ParserFactory factory;

        public Parser(ParserFactory factory)
        {
            this.factory = factory;
        }

        public Principal Parse(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var xmlReaderFactory = new XmlReaderFactory();
                using (var xmlReader = xmlReaderFactory.Instantiate(streamReader))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlReader);

                    var root = xmlDoc.FirstChild.NextSibling;

                    var principalParser = factory.Retrieve<Principal>();
                    var principal = principalParser.Parse(root);
                    return principal;
                }
            }
        }
    }
}
