using IdunnSql.Core.Model;
using IdunnSql.Core.Template.StringTemplate;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdunnSql.Core.Execution
{
    public class ExecutionEngine
    {

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

                Console.Out.WriteLine("Start execution ...");
                server.ConnectionContext.ExecuteNonQuery(script);
                Console.Out.WriteLine("End of execution.");
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

                Console.Out.WriteLine(msg);
            }
        }
        
    }
}
