using Idunn.Console.Parser;
using Idunn.FileShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.FileShare.Parser
{
    class AccountParser : AbstractParser<Account>
    {
        public AccountParser(IParserContainer container, IEngineParser engine)
            : base(container, engine)
        {
        }

        public override Account Parse(object node)
        {
            if (!engine.IsValid(node, "account"))
                throw new ArgumentException();

            var name = engine.GetValue(node, "name");

            var folders = ParseChildren<Folder>(node, new[] { "folder", "folders" });
            var account = new Account(name, folders);
            return account;
        }
    }
}
