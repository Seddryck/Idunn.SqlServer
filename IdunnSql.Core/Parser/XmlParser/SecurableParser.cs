using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IdunnSql.Core.Parser.XmlParser
{
    class SecurableParser
    {
        public Securable Parse(XmlNode node)
        {
            if (node.Name != "securable")
                throw new ArgumentException();

            var name = node.Attributes["name"]?.Value;
            var type = node.Attributes["type"]?.Value;

            var permissions = new List<Permission>();
            foreach (XmlNode child in node.ChildNodes)
            {
                var permissionParser = new PermissionParser();
                permissions.Add(permissionParser.Parse(child));
            }

            var securable = new Securable(name, type, permissions);
            return securable;

        }
    }
}
