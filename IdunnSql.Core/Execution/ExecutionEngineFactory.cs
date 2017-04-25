using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Execution
{
    public class ExecutionEngineFactory
    {
        public ExecutionEngine Instantiate(TextWriter consoleOut, string filename)
        {
            var textWriters = new List<TextWriter>();
            textWriters.Add(consoleOut);

            if (!string.IsNullOrEmpty(filename))
                textWriters.Add(new StreamWriter(filename));

            var engine = new ExecutionEngine(textWriters);
            return engine;
        }
    }
}
