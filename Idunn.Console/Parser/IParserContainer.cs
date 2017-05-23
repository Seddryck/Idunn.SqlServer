using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Parser
{
    public interface IParserContainer
    {
        IParser Retrieve<T>();
        void Initialize(IParserRegister register, IEngineParser engine);
    }
}
