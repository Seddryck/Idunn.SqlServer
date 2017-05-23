using Idunn.Console.Parser;
using Idunn.FileShare.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.FileShare.Parser
{
    public class RootParser : RootParser<Account>
    {
        public RootParser(IParserContainer container, IEngineParser engine) : base(container, engine)
        {
        }

        protected override string[] GetRootNames()
        {
            return new[] { "account", "accounts" };
        }
    }
}
