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
    class FileParser : AbstractParser<File>
    {
        public FileParser(IParserContainer container, IEngineParser engine)
            : base(container, engine)
        {
        }

        public override File Parse(object node)
        {
            if (!engine.IsValid(node, "file"))
                throw new ArgumentException();

            var name = engine.GetValue(node, "name");

            var permissions = ParseChildren<Permission>(node, new[] { "permission", "permissions" });

            var file = new File(name, permissions);
            return file;

        }
    }
}
