using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Console.Execution
{
    public interface IExecutorEngine
    {
        void Execute(IEnumerable<object> objects);
    }

    public interface IExecutorEngine<T> : IExecutorEngine
    {
        void Execute(IEnumerable<T> objects);
    }
}
