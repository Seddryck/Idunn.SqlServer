using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IdunnSql.Core.Parser.XmlParser
{
    class PrincipalParser
    {
        public Principal Parse(XmlNode node)
        {
            if (node.Name != "principal")
                throw new ArgumentException();

            

            var databases = new List<Database>();
            foreach (XmlNode child in node.ChildNodes)
            {
                var databaseParser = new DatabaseParser();
                databases.Add(databaseParser.Parse(child));
            }
            var principal = new Principal(databases);
            return principal;

        }
    }
}
