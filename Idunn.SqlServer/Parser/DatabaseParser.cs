using Idunn.Console.Parser;
using Idunn.SqlServer.Model;
using Idunn.SqlServer.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Parser
{
    class DatabaseParser : AbstractParser<Database>
    {
        public DatabaseParser(IParserContainer container, IEngineParser engine)
            : base(container, engine)
        {
        }

        public override Database Parse(object node)
        {
            if (!engine.IsValid(node, "database"))
                throw new ArgumentException();

            var name = engine.GetValue(node, "name");
            var server = engine.GetValue(node, "server");

            var securables = ParseChildren<Securable>(node, new[] { "securable", "securables" });
            var permissions = ParseChildren<Permission>(node, new[] { "permission", "permissions" });
            
            var database = new Database(name, server, securables, permissions);
        
            return database;
        }

    }
}
