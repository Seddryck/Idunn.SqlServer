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
    class FolderParser : AbstractParser<Folder>
    {
        public FolderParser(IParserContainer container, IEngineParser engine)
            : base(container, engine)
        {
        }

        public override Folder Parse(object node)
        {
            if (!engine.IsValid(node, "folder"))
                throw new ArgumentException();

            var path = engine.GetValue(node, "path");

            var files = ParseChildren<File>(node, new[] { "file", "files" });
            var permissions = ParseChildren<Permission>(node, new[] { "permission", "permissions" });
            
            var database = new Folder(path, files, permissions);
        
            return database;
        }

    }
}
