using Idunn.SqlServer.Core.Model;
using Idunn.SqlServer.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Core.Parser.XmlParser
{
    class DatabaseParser : AbstractParser<Database>
    {
        public DatabaseParser(ParserFactory factory)
            : base(factory)
        {
        }

        public override Database Parse(XmlNode node)
        {
            if (node.Name != "database")
                throw new ArgumentException();

            var name = node.Attributes["name"]?.Value;
            var server = node.Attributes["server"]?.Value;

            var securables = ParseChildren<Securable>(node, "securable");
            var permissions = ParseChildren<Permission>(node, "permission");
            
            var database = new Database(name, server, securables, permissions);
        
            return database;
        }

    }
}
