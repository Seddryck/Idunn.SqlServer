using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idunn.SqlServer.Core.Model
{
    public class Database
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<Securable> Securables { get; set; }
        public IReadOnlyCollection<Permission> Permissions { get; set; }

        public Database(string name, string server, List<Securable> securables, List<Permission> permissions)
        {
            Name = name;
            Server = server;
            Securables = securables ?? new List<Securable>();
            Permissions = permissions ?? new List<Permission>();
        }
    }
}
