using IdunnSql.Core.Model;
using IdunnSql.Core.Template.StringTemplate;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Execution
{
    public class ExecutionEngine
    {
        private readonly IEnumerable<TextWriter> outputs;

        public ExecutionEngine()
            : this (Enumerable.Repeat(Console.Out, 1))
        {

        }

        public ExecutionEngine(IEnumerable<TextWriter> outputs)
        {
            this.outputs = outputs;
        }

        public void Execute(Principal principal)
        {
            foreach (var database in principal.Databases)
            {
                var server = new Server();
                var connectionString = $"Server={database.Server};Initial Catalog={database.Name};Persist Security Info=False;Integrated Security=sspi;";
                server.ConnectionContext.ConnectionString = connectionString;

                var factory = new StringTemplateEngineFactory();
                var engine = factory.Instantiate(principal.Name);
                var script = engine.Execute(principal, false);

                server.ConnectionContext.InfoMessage += new SqlInfoMessageEventHandler(CaptureMessage);

                WriteMessage("Start execution ...");
                server.ConnectionContext.ExecuteNonQuery(script);
                WriteMessage("End of execution.");
                CloseOutputs();
            }
        }

        public void CaptureMessage(object sender, SqlInfoMessageEventArgs e)
        {
            var messages = e.Message.Replace("\r\n", "\r").Split(new[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var msg in messages)
            {
                if (msg.TrimStart().StartsWith("Failure"))
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (msg.TrimStart().StartsWith("Success"))
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (msg.TrimStart().StartsWith("Inconclusive"))
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.White;

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
