using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Parser
{
    public interface IParserRegister
    {
        void Initialize(IParserContainer container, IEngineParser engine);
        IReadOnlyDictionary<Type, object> GetParsers();
        IRootParser GetRootParser();
    }

}
