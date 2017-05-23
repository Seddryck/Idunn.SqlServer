using Idunn.Console.Parser;
using Idunn.FileShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.FileShare.Parser
{
    public class ParserRegister : IParserRegister
    {
        private Dictionary<Type, object> parsers;
        private IRootParser rootParser;

        public void Initialize(IParserContainer container, IEngineParser engine)
        {
            rootParser = new RootParser(container, engine);

            parsers = new Dictionary<Type, object>()
            {
                [typeof(Account)] = new AccountParser(container, engine),
                [typeof(Folder)] = new FolderParser(container, engine),
                [typeof(Permission)] = new PermissionParser(container, engine),
                [typeof(File)] = new FileParser(container, engine),
            };
        }

        public IReadOnlyDictionary<Type, object> GetParsers()
        {
            return parsers;
        }

        public IRootParser GetRootParser()
        {
            return rootParser;
        }
    }
}
