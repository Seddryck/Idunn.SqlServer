using IdunnSql.Core.Model;
using IdunnSql.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IdunnSql.Core.Parser.XmlParser
{
    class DatabaseParser
    {
        public Database Parse(XmlNode node)
        {
            if (node.Name != "database")
                throw new ArgumentException();

            var name = node.Attributes["name"]?.Value;
            var server = node.Attributes["server"]?.Value;

            
            var securables = new List<Securable>();
            foreach (XmlNode child in node.ChildNodes.Cast<XmlNode>().Where(n => n.Name == "securable"))
            {
                var securableParser = new SecurableParser();
                securables.Add(securableParser.Parse(child));
            }

            var permissions = new List<Permission>();
            foreach (XmlNode child in node.ChildNodes.Cast<XmlNode>().Where(n => n.Name == "permission"))
            {
                var permissionParser = new PermissionParser();
                permissions.Add(permissionParser.Parse(child));
            }

            var database = new Database(name, server, securables, permissions);
        
            return database;
        }
    }
}
