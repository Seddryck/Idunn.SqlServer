using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Console.Execution
{
    public interface IExecutorFactory
    {
        IExecutorEngine Instantiate(IEnumerable<TextWriter> textWriters);
    }

    public interface IExecutorFactory<T> : IExecutorFactory
    {
        new IExecutorEngine<T> Instantiate(IEnumerable<TextWriter> textWriters);
    }
}
