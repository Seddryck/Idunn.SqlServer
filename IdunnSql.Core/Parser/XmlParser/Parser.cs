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

                    var principalParser = new PrincipalParser();
                    var principal = principalParser.Parse(root);
                    return principal;
                }
            }
        }
    }
}
