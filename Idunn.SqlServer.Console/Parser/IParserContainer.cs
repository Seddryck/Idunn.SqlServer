using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Console.Parser
{
    public interface IParserContainer
    {
        IParser<T> Retrieve<T>();
        void Initialize(IParserRegister register);
    }
}
