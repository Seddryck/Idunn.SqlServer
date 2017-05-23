using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.Console.Execution
{
    public abstract class ExecutorEngine<T> : IExecutorEngine
    {
        private readonly IEnumerable<TextWriter> outputs;

        public ExecutorEngine()
            : this(Enumerable.Repeat(System.Console.Out, 1))
        {

        }

        public ExecutorEngine(IEnumerable<TextWriter> outputs)
        {
            this.outputs = outputs;
        }

        void IExecutorEngine.Execute(IEnumerable<object> objects)
        {
            Execute(objects.Cast<T>());
        }

        public abstract void Execute(IEnumerable<T> objects);
        

        public void CaptureMessage(object sender, SqlInfoMessageEventArgs e)
        {
            var messages = e.Message.Replace("\r\n", "\r").Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var msg in messages)
            {
                if (msg.TrimStart().StartsWith("Failure"))
                    System.Console.ForegroundColor = ConsoleColor.Red;
                else if (msg.TrimStart().StartsWith("Success"))
                    System.Console.ForegroundColor = ConsoleColor.Green;
                else if (msg.TrimStart().StartsWith("Inconclusive"))
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    System.Console.ForegroundColor = ConsoleColor.White;

                WriteMessage(msg);
            }
        }

        protected void WriteMessage(string message)
        {
            foreach (var output in outputs)
                output.WriteLine(message);
        }

        protected void CloseOutputs()
        {
            foreach (var output in outputs)
            {
                output.Flush();
                output.Close();
            }
        }


    }
}
