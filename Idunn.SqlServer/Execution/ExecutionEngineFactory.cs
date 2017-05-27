using Idunn.Core.Execution;
using Idunn.SqlServer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Execution
{
    [Executor(typeof(Principal))]
    public class ExecutionEngineFactory : IExecutorFactory
    {
        public ExecutorEngine<Principal> Instantiate(IEnumerable<TextWriter> textWriters)
        {
            var engine = new SqlServerEngine(textWriters);
            return engine;
        }

        IExecutorEngine IExecutorFactory.Instantiate(IEnumerable<TextWriter> textWriters)
        {
            return Instantiate(textWriters);
        }
    }
}
