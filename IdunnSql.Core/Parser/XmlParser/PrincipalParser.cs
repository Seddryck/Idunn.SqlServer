using IdunnSql.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IdunnSql.Core.Parser.XmlParser
{
    class PrincipalParser : AbstractParser<Principal>
    {
        public PrincipalParser(ParserFactory factory)
        : base(factory)
        {
        }

        public override Principal Parse(XmlNode node)
        {
            if (node.Name != "principal")
                throw new ArgumentException();

            var databases = ParseChildren<Database>(node, "database");
            var principal = new Principal(databases);
            return principal;

        }
    }
}
