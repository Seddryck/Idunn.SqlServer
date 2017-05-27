using Idunn.Core.Parser;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Idunn.SqlServer.Parser
{
    public class RootParser : RootParser<Principal>
    {
        public RootParser(IParserContainer container, IEngineParser engine) : base(container, engine)
        {
        }

        protected override string[] GetRootNames()
        {
            return new[] { "principal", "principals" };
        }
    }
}
