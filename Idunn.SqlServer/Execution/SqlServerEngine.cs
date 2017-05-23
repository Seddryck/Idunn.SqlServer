using Idunn.Console.Execution;
using Idunn.SqlServer.Model;
using Idunn.SqlServer.Template.StringTemplate;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Execution
{
    public class SqlServerEngine : ExecutorEngine<Principal>
    {
        public SqlServerEngine(IEnumerable<TextWriter> outputs)
            : base(outputs)
        { }

        public override void Execute(IEnumerable<Principal> principals)
        {
            foreach (var principal in principals)
            {
                foreach (var database in principal.Databases)
                {
                    var server = new Server();
                    var connectionString = $"Server={database.Server};Initial Catalog={database.Name};Persist Security Info=False;Integrated Security=sspi;";
                    server.ConnectionContext.ConnectionString = connectionString;

                    var factory = new StringTemplateFactory();
                    var engine = factory.Instantiate(principal.Name, true, string.Empty);
                    var script = engine.Execute(Enumerable.Repeat(principal, 1));

                    server.ConnectionContext.InfoMessage += new SqlInfoMessageEventHandler(CaptureMessage);

                    WriteMessage("Start execution ...");
                    server.ConnectionContext.ExecuteNonQuery(script);
                    WriteMessage("End of execution.");
                    CloseOutputs();
                }
            }
        }
    }
}
