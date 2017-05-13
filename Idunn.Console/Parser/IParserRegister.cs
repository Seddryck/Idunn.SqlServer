using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Parser
{

    public interface IParserRegister
    {
        void Initialize(IParserContainer container);
        IReadOnlyDictionary<Type, object> GetParsers();
        IRootParser GetRootParser();
    }
}
